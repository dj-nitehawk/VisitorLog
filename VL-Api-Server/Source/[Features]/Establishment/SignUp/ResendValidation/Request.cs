using ServiceStack;
using VisitorLog;

namespace Establishment.SignUp.ResendValidation
{
    [Route("/establishment/signup/resend-validation")]
    public class Request : IRequest<Nothing>
    {
        public string Email { get; set; }
    }
}
