using ServiceStack;
using System.Threading.Tasks;
using VisitorLog;

namespace Main.Establishment.SignUp.Validate
{
    [Authenticate(ApplyTo.None)]
    public class Service : Service<Request, Nothing, Database>
    {
        public async Task<Nothing> Post(Request r)
        {
            var email = r.Email.LowerCase();

            if (await Data.TokenIsInvalid(email, r.Code))
                ThrowError("The supplied code is invalid!");

            await Data.ActivateEstablishment(email);

            return Nothing;
        }
    }
}
