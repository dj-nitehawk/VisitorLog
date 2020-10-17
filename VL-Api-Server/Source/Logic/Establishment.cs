using MlkPwgen;
using MongoDB.Entities;
using System.Threading.Tasks;
using VisitorLog;

namespace Logic
{
    public static class Establishment
    {
        public static async Task SendVerificationEmail(string senderName, string senderEmail, string receiverName, string receiverEmail)
        {
            var code = PasswordGenerator.Generate(6, "1234567890");
            await CreateEmailValidationToken(receiverEmail, code);

            var emailMsg = new VisitorLog.Models.Email(
                senderName,
                senderEmail,
                receiverName,
                receiverEmail,
                "Please activate your account...",
                EmailTemplates.Establishment_Email_Verification);

            emailMsg.MergeFields.Add("Salutation", receiverName);
            emailMsg.MergeFields.Add("ValidationCode", code);

            await emailMsg.AddToSendingQueue();

            static Task CreateEmailValidationToken(string email, string code)
            {
                return new Dom.EmailVerificationToken
                {
                    Email = email,
                    Code = int.Parse(code)
                }.SaveAsync();
            }
        }
    }
}
