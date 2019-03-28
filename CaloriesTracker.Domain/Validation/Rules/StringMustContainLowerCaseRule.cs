using CaloriesTracker.Domain.Abstractions.Validation;
using System.Linq;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationResult;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationState;

namespace CaloriesTracker.Domain.Validation.Rules
{
    public sealed class StringMustContainLowerCaseRule : ValidationRule<string>
    {
        protected override ValidationResult CoreCheck(string value)
        {
            var validState = value.Any(x => char.IsLower(x)) ? Valid : Invalid;

            if (validState == Invalid)
            {
                return InvalidResult(new ValidationException("Value string doesn't contain a character in lower case", !string.IsNullOrWhiteSpace(ObjectName) ? ObjectName : "String value"));
            }

            return ValidResult;
        }
    }
}