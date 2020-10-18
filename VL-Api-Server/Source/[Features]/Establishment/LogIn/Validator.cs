using ServiceStack.FluentValidation;
using VisitorLog;

namespace Establishment.LogIn
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email cannot be blank!")
                .EmailAddress().WithMessage("Email format is invalid!");

            RuleFor(x => x.Password)
               .NotEmpty().WithMessage("Password cannot be blank!")
               .Must(x => x.IsAValidPassword()).WithMessage("Password doesn't fit the criteria!");
        }
    }
}
