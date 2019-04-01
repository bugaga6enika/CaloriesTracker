using CaloriesTracker.Domain.Abstractions.Validation;
using System;
using System.Collections.Generic;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationResult;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationState;

namespace CaloriesTracker.Domain.Validation.Rules
{
    public class ValueMustBeGreaterThenRule<T> : ValidationRule<T> //where T : struct
    {
        private readonly T _bottomThreshold;

        public ValueMustBeGreaterThenRule(T bottomThreshold)
        {
            _bottomThreshold = bottomThreshold;
        }

        protected override ValidationResult CoreCheck(T value)
        {
            var validState = value.IsGreaterThen(_bottomThreshold) ? Valid : Invalid;

            if (validState == Invalid)
            {
                return InvalidResult(new ValidationException($"Value must be greater then {_bottomThreshold}", !string.IsNullOrWhiteSpace(ObjectName) ? ObjectName : typeof(T).Name));
            }

            return ValidResult;
        }
    }   

    public static class ValueMustBeGreaterThenRuleExtensions
    {
        public static bool IsGreaterThen<T>(this T @this, T value) //where T : struct
        {
            return Comparer<T>.Default.Compare(@this, value) > 0;
        }
    }
}
