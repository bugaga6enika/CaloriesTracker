using CaloriesTracker.Domain.Abstractions.Validation;
using CaloriesTracker.Domain.Utils;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationResult;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationState;

namespace CaloriesTracker.Domain.Validation.Rules
{
    public sealed class EmailMustBeValidRule : ValidationRule<string>
    {
        protected override ValidationResult CoreCheck(string value)
        {
            var validState = value.IsEmailValid() ? Valid : Invalid;

            if (validState == Invalid)
            {
                return InvalidResult(new ValidationException("Given email is not valid", !string.IsNullOrWhiteSpace(ObjectName) ? ObjectName : "Email value"));
            }

            return ValidResult;
        }
    }

    public static class EmailMustBeValidRuleExtensions
    {
        public static bool IsEmailValid(this string value)
            => RegexCompiler.Current.Email.IsMatch(value);
    }
}
