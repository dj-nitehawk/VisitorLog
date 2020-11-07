using ServiceStack;
using VisitorLog;

namespace Person.Retrieve
{
    [Route("/person/retrieve/{IDNumber}/{PhoneNumber}")]
    public class Request : IRequest<Response>
    {
        public string IDNumber { get; set; }
        public string PhoneNumber { get; set; }

        //public string EstablishmentID; //auto populated from claim
    }
}
