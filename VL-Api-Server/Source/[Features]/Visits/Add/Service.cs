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
            await Data.AddVisit(r.ToVisit(User.ClaimValue(Claim.EstablishmentID)));

            return Nothing;
        }
    }
}
