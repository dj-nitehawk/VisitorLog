using ServiceStack.FluentValidation;

namespace Establishment.SignUp.ResendValidation
{
    public class Validator : AbstractValidator<Request>
    {
        private readonly Database Data = new Database();

        public Validator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email cannot be blank!")
                .EmailAddress().WithMessage("Email format is invalid!")
                .MustAsync(async (_, x, __) => await Data.EstablishmentExists(x)).WithMessage("The email address does not exist!");
        }
    }
}
