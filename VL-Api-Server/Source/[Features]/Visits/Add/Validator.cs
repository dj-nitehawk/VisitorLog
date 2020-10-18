using ServiceStack.FluentValidation;
using VisitorLog;

namespace Visits.Add
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.PhoneNumber).PhoneNumberRule();
            RuleFor(x => x.IDNumber).IDNumberRule();
        }
    }
}
