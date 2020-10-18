using MongoDB.Entities;
using System;

namespace Dom
{
    public class Visit : Entity
    {
        [ObjectId] public string EstablishmentID { get; set; }
        public string PersonID { get; set; }
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
