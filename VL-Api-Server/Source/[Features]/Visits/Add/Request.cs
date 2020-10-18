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

        public Visit ToVisit(string establishmentID) => new Visit
        {
            PersonID = new Dom.Person { IDNumber = IDNumber.UpperCase(), PhoneNumber = PhoneNumber.Trim() }.ID,
            EstablishmentID = establishmentID,
            Date = DateTime.UtcNow,
            Remarks = Remarks,
        };
    }
}
