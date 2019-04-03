using CaloriesTracker.Domain.Abstractions.Core;
using CaloriesTracker.Domain.Abstractions.Validation;
using CaloriesTracker.Domain.Validation.Rules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CaloriesTracker.Domain.User
{
    public sealed class Email : ValueObject
    {
        private readonly IReadOnlyCollection<IValidationRule<string>> _emailValidationRules;

        private Email(string value)
        {
            _emailValidationRules = new ReadOnlyCollection<IValidationRule<string>>
            (
                new IValidationRule<string>[]
                {
                    new StringIsNotNullOrWhiteSpaceRule(),
                    new EmailMustBeValidRule()
                }
            );

            var emailValidationResults = _emailValidationRules.Select(x => x.ApplyTo(value, nameof(Email)));

            if (emailValidationResults.Any(x => x.State == ValidationState.Invalid))
            {
                throw new AggregateException(emailValidationResults.Where(x => x.State == ValidationState.Invalid).Select(x => x.Exception));
            }

            Value = value;
        }

        public string Value { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static Email Parse(string email)
            => new Email(email);

        public static implicit operator string(Email email)
            => email.Value;
    }
}
