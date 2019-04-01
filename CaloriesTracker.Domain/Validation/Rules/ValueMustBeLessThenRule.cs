using CaloriesTracker.Domain.Abstractions.Validation;
using System;
using System.Collections.Generic;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationResult;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationState;

namespace CaloriesTracker.Domain.Validation.Rules
{
    public class ValueMustBeLessThenRule<T> : ValidationRule<T> //where T : struct
    {
        private readonly T _ceilingThreshold;

        public ValueMustBeLessThenRule(T ceilingThreshold)
        {
            _ceilingThreshold = ceilingThreshold;
        }

        protected override ValidationResult CoreCheck(T value)
        {
            var validState = value.IsLessThen(_ceilingThreshold) ? Valid : Invalid;

            if (validState == Invalid)
            {
                return InvalidResult(new ValidationException($"Value must be greater then {_ceilingThreshold}", !string.IsNullOrWhiteSpace(ObjectName) ? ObjectName : typeof(T).Name));
            }

            return ValidResult;
        }
    }

    public static class ValueMustBeLessThenRule
    {
        public static bool IsLessThen<T>(this T @this, T value) //where T : struct
        {
            return Comparer<T>.Default.Compare(@this, value) < 0;
        }
    }
}
