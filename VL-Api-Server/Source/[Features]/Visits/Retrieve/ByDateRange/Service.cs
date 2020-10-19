using System.Collections.Generic;
using System.Threading.Tasks;
using VisitorLog;
using VisitorLog.Auth;

namespace Visits.Retrieve.ByDateRange
{
    public class Service : Service<Request, Nothing, Database>
    {
        public Task<List<VisitItem>> Get(Request r)
        {
            return Data.GetVisits(User.ClaimValue(Claim.EstablishmentID), r.From, r.To, r.PageNo);
        }
    }
}
