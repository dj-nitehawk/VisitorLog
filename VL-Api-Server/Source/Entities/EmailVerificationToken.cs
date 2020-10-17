using MongoDB.Entities;
using System;

namespace Dom
{
    public class EmailVerificationToken : Entity, ICreatedOn
    {
        public DateTime CreatedOn { get; set; }
        public int Code { get; set; }
        public string Email { get; set; }

        static EmailVerificationToken()
        {
            DB.Index<EmailVerificationToken>()
              .Key(x => x.CreatedOn, KeyType.Ascending)
              .Option(o => o.ExpireAfter = TimeSpan.FromHours(4))
              .CreateAsync();
        }
    }
}
