using System.Threading.Tasks;
using VisitorLog;
using VisitorLog.Auth;

namespace Visits.Add
{
    public class Service : Service<Request, Nothing, Database>
    {
        [
            Need(Claim.EstablishmentID)
        ]
        public async Task<Nothing> Post(Request r)
        {
            var visit = r.ToVisit(User.ClaimValue(Claim.EstablishmentID));

            if (!await Data.PersonExists(visit.Person.ID))
                ThrowError("This person doesn't exist in the system! Add them to the system first!");

            await Data.AddVisit(visit);

            return Nothing;
        }
    }
}
