using ServiceStack.FluentValidation;
using VisitorLog;

namespace Establishment.SignUp.Create
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email cannot be blank!")
                .EmailAddress().WithMessage("Email format is invalid!");

            RuleFor(x => x.EmailConfirmation)
                .Equal(x => x.Email).WithMessage("Email confirmation must match email!");

            RuleFor(x => x.Password)
               .NotEmpty().WithMessage("Password cannot be blank!")
               .Must(x => x.IsAValidPassword()).WithMessage("Password doesn't fit the criteria!");

            RuleFor(x => x.PasswordConfirmation)
                .Equal(x => x.Password).WithMessage("Password confirmation must match password!");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Establishment name is required!")
                .MinimumLength(5).WithMessage("Establishment name is not long enough!")
                .MaximumLength(200).WithMessage("Establishment name is too long!");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Establishment type is required!")
                .MinimumLength(3).WithMessage("Establishment type is not long enough!")
                .MaximumLength(100).WithMessage("Establishment type is too long!");

            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Street address is required!");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("District is required!");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required!");

            RuleFor(x => x.CountryCode)
                .NotEmpty().WithMessage("Country is required!");

            RuleFor(v => v.GoogleMapURL)
                .Transform(MapLocation.EmbedCodeToURL)
                .Must(MapLocation.IsEmbedLink).WithMessage("Only google map embed links are valid")
                .Must(MapLocation.IsValid).WithMessage("Could not extract coordinates from map link");

            RuleFor(x => x.ContactName)
                .NotEmpty().WithMessage("Contact name is required!")
                .MinimumLength(5).WithMessage("Contact name is not long enough!")
                .MaximumLength(100).WithMessage("Contact name is too long!");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required!")
                .Must(x => int.TryParse(x, out _) && x.Length == 10).WithMessage("Phone number has to be 10 digits!");
        }
    }
}
