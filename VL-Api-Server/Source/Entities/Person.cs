using MongoDB.Entities;
using System;

namespace Dom
{
    public class Person : Entity, ICreatedOn
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string IDNumber { get; set; }
        public Address Address { get; set; }
        public DateTime CreatedOn { get; set; } //use this to purge of re-request info updates

        static Person()
        {
            DB.Index<Person>()
              .Key(i => i.PhoneNumber, KeyType.Ascending)
              .Key(i => i.IDNumber, KeyType.Ascending)
              .Option(o => o.Unique = true)
              .CreateAsync();
        }
    }
}
