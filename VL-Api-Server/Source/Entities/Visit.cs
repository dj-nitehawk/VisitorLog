using MongoDB.Entities;
using System;

namespace Dom
{
    public class Visit : Entity
    {
        public One<Establishment> Establishment { get; set; }
        public One<Person> Person { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }

        static Visit()
        {
            DB.Index<Visit>()
              .Key(v => v.Date, KeyType.Ascending)
              .Option(o => o.ExpireAfter = TimeSpan.FromDays(90)) //purge visit records after 90 days to reduce disk & index usage
              .CreateAsync();
        }
    }
}
