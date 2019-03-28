using CaloriesTracker.Domain.Abstractions.Validation;
using System.Linq;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationResult;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationState;

namespace CaloriesTracker.Domain.Validation.Rules
{
    public sealed class StringMustContainOnlyDigitsRule : ValidationRule<string>
    {
        protected override ValidationResult CoreCheck(string value)
        {
            var validState = value.All(x => char.IsDigit(x)) ? Valid : Invalid;

            if (validState == Invalid)
            {
                return InvalidResult(new ValidationException("Value string doesn't contain only digits", !string.IsNullOrWhiteSpace(ObjectName) ? ObjectName : "String value"));
            }

            return ValidResult;
        }
    }
}