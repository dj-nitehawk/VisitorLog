using MongoDB.Entities;
using System.Threading.Tasks;
using VisitorLog;

namespace Establishment.LogIn
{
    public class Database : IDatabase
    {
        public Task<Dom.Establishment> GetEstablishment(string email)
        {
            return DB.Find<Dom.Establishment>()
                     .Match(e => e.Email == email)
                     .Project(e => new Dom.Establishment
                     {
                         ID = e.ID,
                         Name = e.Name,
                         PasswordHash = e.PasswordHash,
                         IsEmailVerified = e.IsEmailVerified
                     })
                     .ExecuteSingleAsync();
        }
    }
}
