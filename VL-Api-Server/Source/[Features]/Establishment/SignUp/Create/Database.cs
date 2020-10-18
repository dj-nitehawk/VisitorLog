using MongoDB.Driver;
using MongoDB.Entities;
using System;
using System.Threading.Tasks;
using VisitorLog;

namespace Establishment.SignUp.Create
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
            catch (MongoWriteException x)
            {
                if (x.WriteError.Code == 11000)
                    return false;
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
