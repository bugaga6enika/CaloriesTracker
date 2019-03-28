using CaloriesTracker.Domain.Abstractions.Validation;
using System;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationResult;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationState;

namespace CaloriesTracker.Domain.Validation.Rules
{
    public class DateTimeOffsetMustBeGreaterThenRule : ValidationRule<DateTimeOffset>
    {
        private readonly DateTimeOffset _dateTimeToCompareTo;

        public DateTimeOffsetMustBeGreaterThenRule(DateTimeOffset dateTimeToCompareTo)
        {
            _dateTimeToCompareTo = dateTimeToCompareTo;
        }

        protected override ValidationResult CoreCheck(DateTimeOffset value)
        {
            var validState = value.CompareTo(_dateTimeToCompareTo) <= 0 ? Invalid : Valid;

            if (validState == Invalid)
            {
                return InvalidResult(new ValidationException($"Given date time offset is not after {_dateTimeToCompareTo}", !string.IsNullOrWhiteSpace(ObjectName) ? ObjectName : "Date time value"));
            }

            return ValidResult;
        }
    }
}
