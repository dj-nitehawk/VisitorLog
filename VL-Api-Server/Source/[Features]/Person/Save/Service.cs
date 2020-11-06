using ServiceStack;
using System.Threading.Tasks;
using VisitorLog;

namespace Person.Save
{
    [Authenticate(ApplyTo.None)]
    public class Service : Service<Request, Nothing, Database>
    {
        public async Task<Nothing> Post(Request r)
        {
            await Data.SavePerson(r.ToEntity());

            return Nothing;
        }
    }
}
