using System;
using XForms.Utils.Validation.Contracts;

namespace XForms.Utils.Validation.Rules
{
    public class DateTimeShouldBeBeforeThenRule : IValidationRule<DateTime>
    {
        public string ValidationMessage { get; set; }

        private readonly DateTime _ceilingThreshold;

        public DateTimeShouldBeBeforeThenRule(DateTime ceilingThreshold)
        {
            _ceilingThreshold = ceilingThreshold;
        }

        public bool Check(DateTime value)
        {
            return value.CompareTo(_ceilingThreshold) < 0;
        }
    }
}
