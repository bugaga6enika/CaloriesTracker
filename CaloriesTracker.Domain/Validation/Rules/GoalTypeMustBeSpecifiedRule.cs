using CaloriesTracker.Domain.Abstractions.Validation;
using System;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationResult;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationState;

namespace CaloriesTracker.Domain.Validation.Rules
{
    public sealed class EnumMustBeGreaterThenNullRule<T> : ValidationRule<T> where T : struct, IConvertible
    {
        protected override ValidationResult CoreCheck(T value)
        {
            var validState = value.IsValid() ? Valid : Invalid;

            if (validState == Invalid)
            {
                return InvalidResult(new ValidationException("Goal is not specified", !string.IsNullOrWhiteSpace(ObjectName) ? ObjectName : "Goal value"));
            }

            return ValidResult;
        }
    }

    public static class EnumMustBeGreaterThenNullRuleExtensions
    {
        public static bool IsValid<T>(this T @enum) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            return (int)(object)@enum > 0;
        }
    }
}
