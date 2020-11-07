using Dom;
using MongoDB.Entities;
using System.Threading.Tasks;
using VisitorLog;

namespace Person.Save
{
    public class Database : IDatabase
    {
        public Task SavePerson(Dom.Person person)
        {
            return person.SaveAsync();
        }

        public Task AddVisit(Visit visit)
        {
            return visit.SaveAsync();
        }
    }
}
