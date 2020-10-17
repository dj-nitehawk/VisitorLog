using MongoDB.Driver;
using MongoDB.Entities;
using System;
using System.Threading.Tasks;
using VisitorLog;

namespace Main.Establishment.SignUp.Create
{
    public class Database : IDatabase
    {
        public async Task<bool> CreateEstablishment(Dom.Establishment e)
        {
            try
            {
                await e.SaveAsync();
                return true;
            }
            catch (MongoDuplicateKeyException)
            {
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
