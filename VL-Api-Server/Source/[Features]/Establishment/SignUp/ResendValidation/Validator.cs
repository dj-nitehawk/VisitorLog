using ServiceStack.FluentValidation;
using VisitorLog;

namespace Establishment.SignUp.ResendValidation
{
    public class Validator : AbstractValidator<Request>
    {
        private readonly Database Data = new Database();

        public Validator()
        {
            RuleFor(x => x.Email)
                .EmailAddressRule()
                .MustAsync(async (_, x, __) => await Data.EstablishmentExists(x)).WithMessage("The email address does not exist!");
        }
    }
}
