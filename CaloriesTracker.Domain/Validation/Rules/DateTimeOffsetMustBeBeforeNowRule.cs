using CaloriesTracker.Domain.Abstractions.Validation;
using System;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationResult;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationState;

namespace CaloriesTracker.Domain.Validation.Rules
{
    public sealed class DateTimeOffsetMustBeBeforeNowRule : ValidationRule<DateTimeOffset>
    {
        protected override ValidationResult CoreCheck(DateTimeOffset value)
        {
            var validState = value.CompareTo(DateTimeOffset.Now) >= 0 ? Invalid : Valid;

            if (validState == Invalid)
            {
                return InvalidResult(new ValidationException("Given date time offset is not before now", !string.IsNullOrWhiteSpace(ObjectName) ? ObjectName : "Date time value"));
            }

            return ValidResult;
        }
    }
}
