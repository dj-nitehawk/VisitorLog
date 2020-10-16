﻿using Dom;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VisitorLog.Services;

namespace VisitorLog.Models
{
    public class Email
    {
        private readonly string fromName;
        private readonly string fromEmail;
        private readonly string toName;
        private readonly string toEmail;
        private readonly string subject;
        private string template;

        /// <summary>
        /// A list of merge fields such as: FirstName,John
        /// </summary>
        public Dictionary<string, string> MergeFields { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Instantiate w new email model
        /// </summary>
        public Email(string fromName, string fromEmail, string toName, string toEmail, string subject, EmailTemplates template)
        {
            this.fromName = fromName;
            this.fromEmail = fromEmail;
            this.toName = toName;
            this.toEmail = toEmail;
            this.subject = subject;
            this.template = EmailService.Data.GetTemplate(template);
        }

        /// <summary>
        /// Add this email message to the queue for sending out by the email service.
        /// </summary>
        public Task AddToSendingQueueAsync()
        {
            if (MergeFields.Count == 0) throw new InvalidOperationException("Cannot proceed without any MergeFields!");

            var sb = new StringBuilder(template);

            foreach (var f in MergeFields)
                sb.Replace("{" + f.Key + "}", f.Value);

            template = sb.ToString();

            return EmailService.Data.SaveAsync(ToEntity());
        }

        public EmailMessage ToEntity()
        {
            return new EmailMessage
            {
                FromName = fromName.TitleCase(),
                FromEmail = fromEmail.TitleCase(),
                ToEmail = toEmail.LowerCase(),
                ToName = toName.TitleCase(),
                Subject = subject,
                BodyHTML = template
            };
        }
    }
}
