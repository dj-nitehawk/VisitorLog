using ServiceStack.FluentValidation;
using VisitorLog;

namespace Person.Save
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.FullName).FullNameRule();

            RuleFor(x => x.PhoneNumber).PhoneNumberRule();

            RuleFor(x => x.IDNumber)
                .NotEmpty().WithMessage("ID or Passport number is required!")
                .Must((x, _) => IsAValidID(x.IDNumber, x.IsPassport)).WithMessage("The ID or Passport format is incorrect!");
        }

        private bool IsAValidID(string idNumber, bool isPassport)
        {
            return isPassport
                   ? idNumber.Length >= 5 && idNumber.Length <= 20
                   : idNumber.IsAValidNIC();
        }
    }
}
