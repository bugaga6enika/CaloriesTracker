using CaloriesTracker.Domain.Abstractions.Validation;
using System.Linq;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationResult;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationState;

namespace CaloriesTracker.Domain.Validation.Rules
{
    public sealed class StringMustContainUniqueCharsRule : ValidationRule<string>
    {
        private readonly uint _bottomThreshold;

        public StringMustContainUniqueCharsRule(uint bottomThreshold)
        {
            _bottomThreshold = bottomThreshold;
        }

        protected override ValidationResult CoreCheck(string value)
        {
            var validState = value?.Distinct().Count() >= _bottomThreshold ? Valid : Invalid;

            if (validState == Invalid)
            {
                return InvalidResult(new ValidationException($"Value string must contain at least {_bottomThreshold} chars", !string.IsNullOrWhiteSpace(ObjectName) ? ObjectName : "String value"));
            }

            return ValidResult;
        }
    }
}