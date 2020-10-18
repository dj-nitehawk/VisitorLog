using ServiceStack;
using System.Threading.Tasks;
using VisitorLog;
using VisitorLog.Auth;

namespace Establishment.LogIn
{
    [Authenticate(ApplyTo.None)]
    public class Service : Service<Request, Response, Database>
    {
        public async Task<Response> Post(Request r)
        {
            var estb = await Data.GetEstablishment(r.Email.LowerCase());

            if (!estb.IsEmailVerified)
                ThrowError("Your email address has not yet been verified!");

            if (!BCrypt.Net.BCrypt.Verify(r.Password, estb.PasswordHash))
                ThrowError("The supplied credentials are invalid!");

            Response.SignIn(
                new UserSession((Claim.EstablishmentID, estb.ID)),
                new Allow[0]);

            Response.EstablishmentName = estb.Name;

            return Response;
        }
    }
}
