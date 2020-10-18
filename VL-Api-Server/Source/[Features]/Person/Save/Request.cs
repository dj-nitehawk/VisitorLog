﻿using ServiceStack;
using VisitorLog;

namespace Person.Save
{
    [Route("/person/save")]
    public class Request : IRequest<Dom.Person, Nothing>
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string IDNumber { get; set; }
        public bool IsPassport { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string CountryCode { get; set; }

        public Dom.Person ToEntity()
        {
            return new Dom.Person
            {
                FullName = FullName.TitleCase(),
                PhoneNumber = PhoneNumber,
                Address = new Dom.Address
                {
                    City = City,
                    CountryCode = CountryCode,
                    State = State,
                    Street = Street.TitleCase(),
                    ZipCode = ZipCode
                },
                IDNumber = IDNumber.UpperCase()
            };
        }
    }
}