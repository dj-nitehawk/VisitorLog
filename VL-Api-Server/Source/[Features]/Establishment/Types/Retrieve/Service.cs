using ServiceStack;
using System.Collections.Generic;
using System.Threading.Tasks;
using VisitorLog;

namespace Establishment.Types.Retrieve
{
    [Authenticate(ApplyTo.None)]
    public class Service : Service<Request, Nothing, Database>
    {
        //todo: enable in production [CacheResponse(Duration = 60 * 5)]
        public Task<List<string>> Get(Request _)
        {
            return Logic.Establishment.GetTypeList();
        }
    }
}
