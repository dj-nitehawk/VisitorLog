﻿using Dom;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace VisitorLog.Services
{
    public class EmailService : BackgroundService
    {
        private readonly Settings.EmailSettings settings;
        private readonly bool isProduction;
        private bool startMsgLogged;
        private readonly ILogger log;

        public EmailService(Settings settings, IHostEnvironment environment, ILogger<EmailService> log)
        {
            this.settings = settings.Email;
            isProduction = environment.IsProduction();
            this.log = log;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellation)
        {
            using (var smtp = new SmtpClient())
            {
                var lastActiveAt = DateTime.UtcNow;
                var msgs = new List<EmailMessage>();

                while (isProduction && !cancellation.IsCancellationRequested)
                {
                    if (!startMsgLogged)
                    {
                        startMsgLogged = true;
                        log.LogWarning("EMAIL SERVICE HAS STARTED... [" + settings.Username + "]" + Environment.NewLine);
                    }

                    msgs = await Data.FetchNextBatchAsync(settings.BatchSize);

                    if (msgs.Count > 0)
                    {
                        if (!smtp.IsConnected)
                        {
                            try
                            {
                                await smtp.ConnectAsync(settings.Server, settings.Port, true, cancellation);
                                await smtp.AuthenticateAsync(settings.Username, settings.Password, cancellation);
                                lastActiveAt = DateTime.UtcNow;
                            }
                            catch (Exception x)
                            {
                                log.LogError(x, "COULD NOT CONNECT TO SMTP SERVER SUCCESSFULLY!!! [" + settings.Username + "]" + Environment.NewLine);
                                await Task.Delay((int)TimeSpan.FromMinutes(10).TotalMilliseconds);
                            }
                        }

                        if (smtp.IsConnected && smtp.IsAuthenticated)
                        {
                            foreach (var m in msgs)
                            {
                                try
                                {
                                    await smtp.SendAsync(ComposeEmail(m), cancellation);
                                    lastActiveAt = DateTime.UtcNow;
                                    m.IsSent = true;
                                }
                                catch (Exception x)
                                {
                                    log.LogError(x, "COULD NOT SEND EMAIL VIA SMTP SERVER!!!" + Environment.NewLine);
                                    await Task.Delay((int)TimeSpan.FromMinutes(10).TotalMilliseconds);
                                    break;
                                }
                                finally
                                {
                                    await Data.DeleteMailsAsync(
                                            msgs.Where(m => m.IsSent)
                                                .Select(m => m.ID));
                                }
                            }
                        }
                    }
                    else
                    {
                        if (smtp.IsConnected && DateTime.UtcNow.Subtract(lastActiveAt).TotalSeconds >= 30)
                        {
                            await smtp.DisconnectAsync(true, cancellation);
                        }
                        await Task.Delay((int)TimeSpan.FromSeconds(10).TotalMilliseconds, cancellation);
                    }
                }
            }
        }

        private MimeMessage ComposeEmail(EmailMessage msg)
        {
            var m = new MimeMessage();

            m.From.Add(new MailboxAddress(settings.FromName, settings.FromEmail));
            m.To.Add(new MailboxAddress(msg.ToName, msg.ToEmail));
            m.Subject = msg.Subject;
            m.Body = new TextPart(TextFormat.Html)
            {
                Text = msg.BodyHTML
            };
            return m;
        }

        public static class Data
        {
            public static Task<List<EmailMessage>> FetchNextBatchAsync(int batchSize)
            {
                return DB.Find<EmailMessage>()
                         .Limit(batchSize)
                         .ExecuteAsync();
            }

            public static Task DeleteMailsAsync(IEnumerable<string> mailIDs)
            {
                return DB.DeleteAsync<EmailMessage>(mailIDs);
            }

            public static string GetTemplate(EmailTemplates template)
            {
                //fetch from db after they can be edited from admin backend.
                return template switch
                {
                    EmailTemplates.Account_Welcome => @"
                    <html>
                    <body>
                      <div>
                        <p>Dear {Salutation}</p>
                        <p>Your account has been created.</p>
                        <p>In order to complete the sign-up process please click the link below:</p>
                        <a href='{ValidationLink}'>Click here to continue!</a>
                        <p>Thank you!</p>
                      </div>
                    </body>
                    </html>",
                    _ => null,
                };
            }

            public static Task SaveAsync(EmailMessage emailMessage)
            {
                return emailMessage.SaveAsync();
            }
        }
    }
}
