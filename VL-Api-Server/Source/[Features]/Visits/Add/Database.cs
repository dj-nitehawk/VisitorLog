using Dom;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Entities;
using System.Threading.Tasks;
using VisitorLog;

namespace Visits.Add
{
    public class Database : IDatabase
    {
        public async Task<bool> PersonExists(string personID)
        {
            return
                await DB.Queryable<Dom.Person>()
                        .AnyAsync(p => p.ID == personID);
        }

        public Task AddVisit(Visit visit)
        {
            return visit.SaveAsync();
        }
    }
}
