using CaloriesTracker.Domain.Abstractions.Validation;
using System;
using System.Collections.Generic;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationResult;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationState;

namespace CaloriesTracker.Domain.Validation.Rules
{
    public class ValueMustBeLessThenRule<T> : ValidationRule<T> where T : IComparable<T>
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

    public class ValueMustBeLessThenIfRule<T> : ValidationRule<T> where T : IComparable<T>
    {
        private readonly Func<T> _ceilingThresholdRef;
        private readonly Func<bool> _conditionToCheck;

        public ValueMustBeLessThenIfRule(Func<T> ceilingThresholdRef, Func<bool> conditionToCheck, string reference)
        {
            _ceilingThresholdRef = ceilingThresholdRef;
            _conditionToCheck = conditionToCheck;
        }

        protected override ValidationResult CoreCheck(T value)
        {
            var validState = _conditionToCheck.Invoke()
                ? value.IsLessThen(_ceilingThresholdRef.Invoke()) ? Valid : Invalid
                : Valid;

            if (validState == Invalid)
            {
                return InvalidResult(new ValidationException($"Value must be less then {_ceilingThresholdRef.Invoke()}", !string.IsNullOrWhiteSpace(ObjectName) ? ObjectName : typeof(T).Name));
            }

            return ValidResult;
        }
    }

    public static class ValueMustBeLessThenRule
    {
        public static bool IsLessThen<T>(this T @this, T value) where T : IComparable<T>
        {
            return Comparer<T>.Default.Compare(@this, value) < 0;
        }
    }
}
