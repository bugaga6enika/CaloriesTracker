using System.Collections.Generic;
using XForms.Utils.Validation.Contracts;

namespace XForms.Utils.Validation.Rules
{
    public class ValueShouldBeGreaterThenRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        private readonly T _bottomThreshold;

        public ValueShouldBeGreaterThenRule(T bottomThreshold)
        {
            _bottomThreshold = bottomThreshold;
            ValidationMessage = $"Value should be greater then {bottomThreshold}";
        }

        public bool Check(T value)
        {
            return Comparer<T>.Default.Compare(value, _bottomThreshold) > 0;
        }
    }

    public class IntValueShouldBeGreaterThenRule : IValidationRule<int?>
    {
        public string ValidationMessage { get; set; }
        private readonly int _bottomThreshold;

        public IntValueShouldBeGreaterThenRule(int bottomThreshold)
        {
            _bottomThreshold = bottomThreshold;
            ValidationMessage = $"Value should be greater then {bottomThreshold}";
        }

        public bool Check(int? value)
        {
            return value.HasValue && value > _bottomThreshold;
        }
    }

    public class DoubleValueShouldBeGreaterThenRule : IValidationRule<double?>
    {
        public string ValidationMessage { get; set; }
        private readonly double _bottomThreshold;

        public DoubleValueShouldBeGreaterThenRule(double bottomThreshold)
        {
            _bottomThreshold = bottomThreshold;
            ValidationMessage = $"Value should be greater then {bottomThreshold}";
        }

        public bool Check(double? value)
        {
            return value.HasValue && value > _bottomThreshold;
        }
    }
}
