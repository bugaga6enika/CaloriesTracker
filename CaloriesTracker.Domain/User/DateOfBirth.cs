using CaloriesTracker.Domain.Abstractions.Core;
using CaloriesTracker.Domain.Abstractions.Validation;
using CaloriesTracker.Domain.Validation.Rules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CaloriesTracker.Domain.User
{
    public sealed class DateOfBirth : ValueObject
    {
        private readonly IReadOnlyCollection<IValidationRule<DateTimeOffset>> _dateOfBirthValidationRules;

        private DateOfBirth()
        {
            _dateOfBirthValidationRules = new ReadOnlyCollection<IValidationRule<DateTimeOffset>>(new IValidationRule<DateTimeOffset>[]
            {
                  new DateTimeOffsetMustBeBeforeNowRule()
            });
        }

        private DateOfBirth(DateTimeOffset value) : this()
        {
            var dateOfBirthValidationResults = _dateOfBirthValidationRules.Select(x => x.ApplyTo(value, "Date of birth"));

            if (dateOfBirthValidationResults.Any(x => x.State == ValidationState.Invalid))
            {
                throw new AggregateException(dateOfBirthValidationResults.Where(x => x.State == ValidationState.Invalid).Select(x => x.Exception));
            }

            Value = value;
        }

        public DateTimeOffset Value { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static DateOfBirth Create(DateTimeOffset dateTime)
            => new DateOfBirth(dateTime);
    }
}
