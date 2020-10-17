using Dom;
using MongoDB.Entities;
using System.Threading.Tasks;
using VisitorLog;

namespace Main.Establishment.SignUp.Validate
{
    public class Database : IDatabase
    {
        public async Task<bool> TokenIsInvalid(string email, int code)
        {
            return (
                await DB.DeleteAsync<EmailVerificationToken>(
                    t =>
                    t.Email == email &&
                    t.Code == code))
                .DeletedCount == 0;
        }

        public Task ActivateEstablishment(string email)
        {
            return DB.Update<Dom.Establishment>()
                     .Match(e => e.Email == email)
                     .Modify(e => e.IsEmailVerified, true)
                     .ExecuteAsync();
        }
    }
}
