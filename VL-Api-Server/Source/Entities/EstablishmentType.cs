using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;
using System;
using System.Linq;
using VisitorLog;

namespace Dom
{
    public class EstablishmentType : IEntity
    {
        [BsonId] public string ID { get; set; }
        public string Name { get; set; }

        public EstablishmentType(string name)
        {
            if (name.HasNoValue())
                throw new ArgumentNullException($"Please set a value for {nameof(Name)} before calling this method!");

            Name = name;
            SetNewID();
        }

        public void SetNewID()
        {
            ID = string.Join("", Name.LowerCase().Where(x => char.IsLetterOrDigit(x)));
        }
    }
}
