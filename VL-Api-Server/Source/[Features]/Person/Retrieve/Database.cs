using MongoDB.Entities;
using System.Threading.Tasks;
using VisitorLog;

namespace Person.Retrieve
{
    public class Database : IDatabase
    {
        public Task<Response> GetPerson(string idNumber, string phoneNumber)
        {
            var id = new Dom.Person { IDNumber = idNumber, PhoneNumber = phoneNumber }.ID;

            return DB.Find<Dom.Person, Response>()
                     .Match(p => p.ID == id)
                     .Project(p => new Response
                     {
                         IDNumber = p.IDNumber,
                         PhoneNumber = p.PhoneNumber,
                         FullName = p.FullName,
                         Address = p.Address.Street + ", " + p.Address.City + ", " + p.Address.State
                     })
                     .ExecuteSingleAsync();
        }
    }
}
