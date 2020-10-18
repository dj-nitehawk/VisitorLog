using ServiceStack.FluentValidation;
using VisitorLog;

namespace Establishment.LogIn
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Email).EmailAddressRule();
            RuleFor(x => x.Password).PasswordRule();
        }
    }
}
