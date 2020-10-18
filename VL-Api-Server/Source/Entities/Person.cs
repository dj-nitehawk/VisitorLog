using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;
using System;

namespace Dom
{
    public class Person : IEntity, IModifiedOn
    {
        [BsonElement, BsonId]
        public string ID
        {
            get => $"{IDNumber}.{PhoneNumber}";
            set => throw new InvalidOperationException("Person IDs are auto generated!");
        }

        public string PhoneNumber { get; set; }
        public string IDNumber { get; set; }
        public string FullName { get; set; }
        public Address Address { get; set; }
        public DateTime ModifiedOn { get; set; } //use this to purge or re-request info updates in the future
    }
}
