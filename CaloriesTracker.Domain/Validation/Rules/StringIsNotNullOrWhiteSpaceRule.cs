using CaloriesTracker.Domain.Abstractions.Validation;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationResult;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationState;

namespace CaloriesTracker.Domain.Validation.Rules
{
    public sealed class StringIsNotNullOrWhiteSpaceRule : ValidationRule<string>
    {
        protected override ValidationResult CoreCheck(string value)
        {
            var validState = string.IsNullOrWhiteSpace(value) ? Invalid : Valid;

            if (validState == Invalid)
            {
                return InvalidResult(new ValidationException("Value string is null or empty", !string.IsNullOrWhiteSpace(ObjectName) ? ObjectName : "String value"));
            }

            return ValidResult;
        }
    }
}