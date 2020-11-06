using MongoDB.Entities;
using System.Threading.Tasks;
using VisitorLog;

namespace Person.Retrieve
{
    public class Database : IDatabase
    {
        public Task<Response> GetPerson(string idNumber, string phoneNumber)
        {
            var personID = new Dom.Person(idNumber, phoneNumber).ID;

            return DB.Find<Dom.Person, Response>()
                     .Match(p => p.ID == personID)
                     .Project(p => new Response
                     {
                         IDNumber = p.IDNumber,
                         PhoneNumber = p.PhoneNumber,
                         FullName = p.FullName,
                     })
                     .ExecuteSingleAsync();
        }
    }
}
