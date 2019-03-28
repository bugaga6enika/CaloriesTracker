using CaloriesTracker.Domain.Abstractions.Validation;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationResult;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationState;

namespace CaloriesTracker.Domain.Validation.Rules
{
    public sealed class StringMinLengthRule : ValidationRule<string>
    {
        private readonly uint _bottomThreshold;

        public StringMinLengthRule(uint bottomThreshold)
        {
            _bottomThreshold = bottomThreshold;
        }

        protected override ValidationResult CoreCheck(string value)
        {
            var validationState = value.DoesGreaterOrEqualThen(_bottomThreshold) ? Valid : Invalid;

            if (validationState == Invalid)
            {
                return InvalidResult(new ValidationException($"String value length of {value?.Length} is less then {_bottomThreshold}.", !string.IsNullOrWhiteSpace(ObjectName) ? ObjectName : "String value"));
            }

            return ValidResult;
        }
    }

    public static class StringMinLengthRuleExtensions
    {
        public static bool DoesGreaterThen(this string value, uint bottomThreshold)
            => (value?.Length ?? 0) > bottomThreshold;

        public static bool DoesGreaterOrEqualThen(this string value, uint bottomThreshold)
            => (value?.Length ?? 0) >= bottomThreshold;
    }
}
