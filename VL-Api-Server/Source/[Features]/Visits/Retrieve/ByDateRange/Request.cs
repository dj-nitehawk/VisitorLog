using ServiceStack;
using VisitorLog;

namespace Visits.Retrieve.ByDateRange
{
    [Route("/visits/retrieve/by-date-range/{From}/{To}/{PageNo}")]
    public class Request : IRequest<Nothing>
    {
        public string From { get; set; }
        public string To { get; set; }
        public int PageNo { get; set; } = 1;

        public string EstablishmentID; //auto populated from claim
    }
}
