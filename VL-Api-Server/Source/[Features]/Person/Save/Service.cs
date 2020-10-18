using System.Threading.Tasks;
using VisitorLog;
using VisitorLog.Auth;

namespace Person.Save
{
    public class Service : Service<Request, Nothing, Database>
    {
        [
            Need(Claim.EstablishmentID)
        ]
        public async Task<Nothing> Post(Request r)
        {
            await Data.SavePerson(r.ToEntity());

            return Nothing;
        }
    }
}
