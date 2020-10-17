using ServiceStack;
using System.Threading.Tasks;
using VisitorLog;

namespace Main.Establishment.SignUp.ResendValidation
{
    [Authenticate(ApplyTo.None)]
    public class Service : Service<Request, Nothing, Database>
    {
        public async Task<Nothing> Post(Request r)
        {
            await Logic.Establishment.SendVerificationEmail(
                Settings.Email.FromName,
                Settings.Email.FromEmail,
                "User",
                r.Email.LowerCase());

            return Nothing;
        }
    }
}
