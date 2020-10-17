using System.Threading.Tasks;
using VisitorLog;

namespace Main.Establishment.SignUp.Create
{
    public class Service : Service<Request, Nothing, Database>
    {
        public async Task<Nothing> Post(Request r)
        {
            if (await Data.CreateEstablishment(r.ToEntity()))
                return Nothing;

            ThrowError(r => r.Email, "The supplied email address is already in use!");

            return Nothing;
        }
    }
}
