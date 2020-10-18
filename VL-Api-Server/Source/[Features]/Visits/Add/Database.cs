using Dom;
using MongoDB.Entities;
using System.Threading.Tasks;
using VisitorLog;

namespace Visits.Add
{
    public class Database : IDatabase
    {
        public Task AddVisit(Visit visit)
        {
            return visit.SaveAsync();
        }
    }
}
