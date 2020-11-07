using ServiceStack;
using System.Threading.Tasks;
using VisitorLog;
using VisitorLog.Auth;

namespace Person.Save
{
    [Authenticate(ApplyTo.None)]
    public class Service : Service<Request, Nothing, Database>
    {
        public async Task<Nothing> Post(Request r)
        {
            await Data.SavePerson(r.ToEntity());

            var establishmentID = User.ClaimValue(Claim.EstablishmentID);

            if (establishmentID != null)
            {
                await Data.AddVisit(r.ToVisit(establishmentID));
            }

            return Nothing;
        }
    }
}
