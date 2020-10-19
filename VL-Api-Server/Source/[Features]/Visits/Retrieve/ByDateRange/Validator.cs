using ServiceStack.FluentValidation;
using VisitorLog;

namespace Visits.Retrieve.ByDateRange
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.From).DateStringRule();
            RuleFor(x => x.To).DateStringRule();
        }
    }
}
