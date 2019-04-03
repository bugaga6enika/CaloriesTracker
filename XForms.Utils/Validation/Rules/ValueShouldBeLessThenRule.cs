using System;
using System.Collections.Generic;
using XForms.Utils.Validation.Contracts;

namespace XForms.Utils.Validation.Rules
{
    public class ValueShouldBeLessThenRule<T> : IValidationRule<T> where T : IComparable<T>
    {
        public string ValidationMessage { get; set; }
        private readonly T _ceilingThreshold;

        public ValueShouldBeLessThenRule(T ceilingThreshold)
        {
            _ceilingThreshold = ceilingThreshold;
            ValidationMessage = $"Value should be less then {ceilingThreshold}";
        }

        public bool Check(T value)
        {
            return Comparer<T>.Default.Compare(value, _ceilingThreshold) < 0;
        }
    }

    public class ValueShouldBeLessThenRule : IValidationRule<int?>
    {
        public string ValidationMessage { get; set; }
        private readonly int _ceilingThreshold;

        public ValueShouldBeLessThenRule(int ceilingThreshold)
        {
            _ceilingThreshold = ceilingThreshold;
            ValidationMessage = $"Value should be less then {ceilingThreshold}";
        }

        public bool Check(int? value)
        {
            return value.HasValue && value < _ceilingThreshold;
        }
    }

    public class DoubleValueShouldBeLessThenRule : IValidationRule<double?>
    {
        public string ValidationMessage { get; set; }
        private readonly double _ceilingThreshold;

        public DoubleValueShouldBeLessThenRule(double ceilingThreshold)
        {
            _ceilingThreshold = ceilingThreshold;
            ValidationMessage = $"Value should be less then {ceilingThreshold}";
        }

        public bool Check(double? value)
        {
            return value.HasValue && value < _ceilingThreshold;
        }
    }

    public class DoubleValueShouldBeLessThenIfRule<T> : IValidationRule<T> where T : IComparable<T>
    {
        public string ValidationMessage { get; set; }

        private readonly Func<T> _ceilingThresholdRef;
        private readonly Func<bool> _conditionToCheck;

        public DoubleValueShouldBeLessThenIfRule(Func<T> ceilingThresholdRef, Func<bool> conditionToCheck, string reference)
        {
            _ceilingThresholdRef = ceilingThresholdRef;
            _conditionToCheck = conditionToCheck;
            ValidationMessage = $"Value should be less then {reference}";
        }

        public bool Check(T value)
        {
            return _conditionToCheck.Invoke() ? Comparer<T>.Default.Compare(value, _ceilingThresholdRef.Invoke()) < 0 : true;
        }
    }

    public class DoubleValueShouldBeLessThenIfRule : IValidationRule<double?>
    {
        public string ValidationMessage { get; set; }

        private readonly Func<double?> _ceilingThresholdRef;
        private readonly Func<bool> _conditionToCheck;

        public DoubleValueShouldBeLessThenIfRule(Func<double?> ceilingThresholdRef, Func<bool> conditionToCheck, string reference)
        {
            _ceilingThresholdRef = ceilingThresholdRef;
            _conditionToCheck = conditionToCheck;
            ValidationMessage = $"Value should be less then {reference}";
        }

        public bool Check(double? value)
        {
            return _conditionToCheck.Invoke() ? value < _ceilingThresholdRef.Invoke() : true;
        }
    }
}
