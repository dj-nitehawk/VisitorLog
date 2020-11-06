using ServiceStack;
using System.Threading.Tasks;
using VisitorLog;
using VisitorLog.Auth;

namespace Person.Retrieve
{
    public class Service : Service<Request, Response, Database>
    {
        [
            Need(Claim.EstablishmentID)
        ]
        public async Task<Response> Get(Request r)
        {
            Response = await Data.GetPerson(r.IDNumber.UpperCase(), r.PhoneNumber.Trim());

            if (Response == null)
                throw HttpError.NotFound("Person not found!");

            return Response;
        }
    }
}
