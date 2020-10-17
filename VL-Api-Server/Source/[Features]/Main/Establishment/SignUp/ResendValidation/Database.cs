using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Entities;
using System.Threading.Tasks;
using VisitorLog;

namespace Main.Establishment.SignUp.ResendValidation
{
    public class Database : IDatabase
    {
        public Task<bool> EstablishmentExists(string email)
        {
            return DB.Queryable<Dom.Establishment>()
                     .AnyAsync(e => e.Email == email);
        }
    }
}
