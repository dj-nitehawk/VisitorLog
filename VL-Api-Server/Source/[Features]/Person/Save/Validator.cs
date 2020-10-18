using ServiceStack.FluentValidation;
using VisitorLog;

namespace Person.Save
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Name cannot be empty!")
                .MinimumLength(5).WithMessage("Name is too short!")
                .MaximumLength(100).WithMessage("Name is too long!");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required!")
                .Must(x => int.TryParse(x, out _) && x.Length == 10).WithMessage("Phone number has to be 10 digits!");

            RuleFor(x => x.IDNumber)
                .NotEmpty().WithMessage("ID or Passport number is required!")
                .Must((x, _) => IsAValidID(x.IDNumber, x.IsPassport)).WithMessage("The ID or Passport format is incorrect!");
        }

        private bool IsAValidID(string idNumber, bool isPassport)
        {
            return isPassport
                   ? idNumber.Length > 5 && idNumber.Length <= 20
                   : idNumber.IsAValidNIC();
        }
    }
}
