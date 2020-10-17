using ServiceStack;
using System.Threading.Tasks;
using VisitorLog;

namespace Main.Establishment.SignUp.Create
{
    [Authenticate(ApplyTo.None)]
    public class Service : Service<Request, Nothing, Database>
    {
        public async Task<Nothing> Post(Request r)
        {
            _ = Logic.Establishment.AddToTypeList(r.Type.TitleCase()); //todo: add this line when updating establishment details as well

            if (!await Data.CreateEstablishment(r.ToEntity()))
                ThrowError(r => r.Email, "The supplied email address is already in use!");

            await Logic.Establishment.SendVerificationEmail(
                Settings.Email.FromName,
                Settings.Email.FromEmail,
                r.ContactName,
                r.Email);

            return Nothing;
        }
    }
}
