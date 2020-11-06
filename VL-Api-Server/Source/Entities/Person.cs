using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;
using System;
using VisitorLog;

namespace Dom
{
    public class Person : IEntity, IModifiedOn
    {
        private readonly string IDNumber;
        private readonly string PhoneNumber;

        [BsonId]
        public string ID { get; set; }
        public string FullName { get; set; }
        public Address Address { get; set; }
        public DateTime ModifiedOn { get; set; } //use this to purge or re-request info updates in the future

        public Person(string idNumber, string phoneNumber)
        {
            IDNumber = idNumber;
            PhoneNumber = phoneNumber;
            SetNewID();
        }

        public void SetNewID()
        {
            if (IDNumber.HasNoValue() || PhoneNumber.HasNoValue())
                throw new ArgumentNullException($"Please set both {nameof(IDNumber)} and {nameof(PhoneNumber)} first!");

            ID = $"{IDNumber}.{PhoneNumber}";
        }
    }
}
