using CaloriesTracker.Domain.Abstractions.Validation;
using System.Linq;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationResult;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationState;

namespace CaloriesTracker.Domain.Validation.Rules
{
    public sealed class StringMustContainNonAlphanumericRule : ValidationRule<string>
    {
        protected override ValidationResult CoreCheck(string value)
        {
            var validState = value.DoesContainNonAlphanumeric() ? Valid : Invalid;

            if (validState == Invalid)
            {
                return InvalidResult(new ValidationException("Value string doesn't contain a non alphanumeric character", !string.IsNullOrWhiteSpace(ObjectName) ? ObjectName : "String value"));
            }

            return ValidResult;
        }
    }

    public static class StringMustContainNonAlphanumericRuleExtensions
    {
        public static bool DoesContainNonAlphanumeric(this string value)
            => value?.Any(x => !char.IsLetterOrDigit(x)) ?? false;
    }
}