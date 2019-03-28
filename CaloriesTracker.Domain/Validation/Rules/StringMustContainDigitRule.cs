using CaloriesTracker.Domain.Abstractions.Validation;
using System.Linq;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationResult;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationState;

namespace CaloriesTracker.Domain.Validation.Rules
{
    public sealed class StringMustContainDigitRule : ValidationRule<string>
    {
        protected override ValidationResult CoreCheck(string value)
        {
            var validState = value.DoesContainDigit() ? Valid : Invalid;

            if (validState == Invalid)
            {
                return InvalidResult(new ValidationException("Value string doesn't contain a digit", !string.IsNullOrWhiteSpace(ObjectName) ? ObjectName : "String value"));
            }

            return ValidResult;
        }
    }

    public static class StringMustContainDigitRuleExtensions
    {
        public static bool DoesContainDigit(this string value)
            => value?.Any(x => char.IsDigit(x)) ?? false;
    }
}