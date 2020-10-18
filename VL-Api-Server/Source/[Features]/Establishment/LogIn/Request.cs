using ServiceStack;
using VisitorLog;

namespace Establishment.LogIn
{
    [Route("/establishment/login")]
    public class Request : IRequest<Response>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
