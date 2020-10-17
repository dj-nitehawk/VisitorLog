
using MlkPwgen;
using System.Threading.Tasks;
using VisitorLog;

namespace Main.Establishment.SignUp.Create
{
    public class Service : Service<Request, Nothing, Database>
    {
        public async Task<Nothing> Post(Request r)
        {
            if (!await Data.CreateEstablishment(r.ToEntity()))
                ThrowError(r => r.Email, "The supplied email address is already in use!");

            var code = PasswordGenerator.Generate(6, "1234567890");
            await Data.CreateEmailValidationToken(r.Email, code);

            var emailMsg = new VisitorLog.Models.Email(
                Settings.Email.FromName,
                Settings.Email.FromEmail,
                r.ContactName,
                r.Email,
                "Please activate your account...",
                EmailTemplates.Establishment_Email_Verification);

            emailMsg.MergeFields.Add("Salutation", r.ContactName);
            emailMsg.MergeFields.Add("ValidationCode", code);

            await emailMsg.AddToSendingQueue();

            return Nothing;
        }
    }
}
