using Dom;
using ServiceStack;
using System;
using VisitorLog;

namespace Visits.Add
{
    [Route("/visits/add")]
    public class Request : IRequest<Nothing>
    {
        public string IDNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Remarks { get; set; }

        public string EstablishmentID; //auto populated from claim

        public Visit ToVisit() => new Visit
        {
            Person = new Dom.Person(IDNumber.UpperCase(), PhoneNumber.Trim()),
            Establishment = EstablishmentID,
            Date = DateTime.UtcNow,
            Remarks = Remarks,
        };
    }
}
