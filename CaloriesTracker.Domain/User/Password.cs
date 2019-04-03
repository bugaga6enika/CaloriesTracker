using CaloriesTracker.Domain.Abstractions.Core;
using CaloriesTracker.Domain.Abstractions.Validation;
using CaloriesTracker.Domain.Validation.Rules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CaloriesTracker.Domain.User
{
    public sealed class Password : ValueObject
    {
        private readonly IReadOnlyCollection<IValidationRule<string>> _passwordValidationRules;

        private Password()
        {
            _passwordValidationRules = new ReadOnlyCollection<IValidationRule<string>>
            (
                new IValidationRule<string>[]
                {
                    new StringIsNotNullOrWhiteSpaceRule(),
                    new StringMinLengthRule(10),
                    new StringMustContainDigitRule(),
                    new StringMustContainLowerCaseRule(),
                    new StringMustContainUpperCaseRule(),
                    new StringMustContainUniqueCharsRule(2),
                    new StringMustContainNonAlphanumericRule()
                }
            );
        }

        private Password(string value) : this()
        {
            var passwordValidationResults = _passwordValidationRules.Select(x => x.ApplyTo(value, nameof(Email)));

            if (passwordValidationResults.Any(x => x.State == ValidationState.Invalid))
            {
                throw new AggregateException(passwordValidationResults.Where(x => x.State == ValidationState.Invalid).Select(x => x.Exception));
            }

            Value = value;
        }

        public string Value { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static Password Parse(string password)
            => new Password(password);

        public static implicit operator string(Password password)
            => password.Value;
    }
}
