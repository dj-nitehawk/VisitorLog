using MongoDB.Entities;
using System;
using System.Linq;
using VisitorLog;

namespace Dom
{
    public class EstablishmentType : IEntity
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public void PopulateID()
        {
            if (Name.HasNoValue())
                throw new ArgumentNullException($"Please set a value for {nameof(Name)} before calling this method!");

            ID = string.Join("", Name.LowerCase().Where(x => char.IsLetterOrDigit(x)));
        }
    }
}
