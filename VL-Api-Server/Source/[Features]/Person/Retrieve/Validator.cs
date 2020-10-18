using ServiceStack.FluentValidation;

namespace Person.Retrieve
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required!")
                .Must(x => int.TryParse(x, out _) && x.Length == 10).WithMessage("Phone number has to be 10 digits!");

            RuleFor(x => x.IDNumber)
                .NotEmpty().WithMessage("ID or Passport number is required!")
                .Must((x, _) => IsAValidID(x.IDNumber)).WithMessage("The ID or Passport format is incorrect!");
        }

        private bool IsAValidID(string idNumber)
        {
            return idNumber.Length > 5 && idNumber.Length <= 20;
        }
    }
}
