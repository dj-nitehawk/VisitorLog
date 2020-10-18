using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;
using System;
using VisitorLog;

namespace Dom
{
    public class Person : IEntity, IModifiedOn
    {
        [BsonElement, BsonId]
        public string ID
        {
            get
            {
                if (IDNumber.HasNoValue() || PhoneNumber.HasNoValue())
                    throw new ArgumentNullException($"Please set both {nameof(IDNumber)} and {nameof(PhoneNumber)} first!");
                else
                    return $"{IDNumber}.{PhoneNumber}";
            }

            set => throw new InvalidOperationException($"Person IDs are auto generated! Set {nameof(IDNumber)} and {nameof(PhoneNumber)} instead!");
        }

        public string PhoneNumber { get; set; }
        public string IDNumber { get; set; }
        public string FullName { get; set; }
        public Address Address { get; set; }
        public DateTime ModifiedOn { get; set; } //use this to purge or re-request info updates in the future
    }
}
