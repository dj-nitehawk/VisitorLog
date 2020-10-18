using ServiceStack;
using ServiceStack.FluentValidation;
using ServiceStack.Host;
using ServiceStack.Validation;
using System.Linq;

namespace VisitorLog
{
    public static class Validation
    {
        public static HandleServiceExceptionDelegate CustomHandler = (_, __, x) =>
        {
            if (x is ValidationError ex)
            {
                return new HttpError(
                    new
                    {
                        errors = ex.Violations
                                   .GroupBy(f => f.FieldName)
                                   .ToDictionary(x => x.Key,
                                                 x => x.Select(e => e.ErrorMessage)),
                        status = 400,
                        title = "One or more validation errors occurred."
                    },
                    400,
                    "Validation Error");
            }

            return null;
        };

        public static IRuleBuilderOptions<T, string> EmailAddressRule<T>(this IRuleBuilder<T, string> builder)
        {
            return builder
                .NotEmpty().WithMessage("Email cannot be blank!")
                .EmailAddress().WithMessage("Email format is invalid!");
        }

        public static IRuleBuilderOptions<T, string> PasswordRule<T>(this IRuleBuilder<T, string> builder)
        {
            return builder
                .NotEmpty().WithMessage("Password cannot be blank!")
                .Must(x => x.IsAValidPassword()).WithMessage("Password doesn't fit the criteria!");
        }

        public static IRuleBuilderOptions<T, string> PhoneNumberRule<T>(this IRuleBuilder<T, string> builder)
        {
            return builder
                .NotEmpty().WithMessage("Phone number is required!")
                .Must(x => int.TryParse(x, out _) && x.Length == 10).WithMessage("Phone number has to be 10 digits!");
        }

        public static IRuleBuilderOptions<T, string> IDNumberRule<T>(this IRuleBuilder<T, string> builder)
        {
            return builder
                .NotEmpty().WithMessage("ID or Passport number is required!")
                .Must(x => x.Length >= 5 && x.Length <= 20).WithMessage("The ID or Passport format is incorrect!");
        }

        public static IRuleBuilderOptions<T, string> FullNameRule<T>(this IRuleBuilder<T, string> builder)
        {
            return builder
                .NotEmpty().WithMessage("Name cannot be empty!")
                .MinimumLength(5).WithMessage("Name is too short!")
                .MaximumLength(100).WithMessage("Name is too long!");
        }
    }
}
