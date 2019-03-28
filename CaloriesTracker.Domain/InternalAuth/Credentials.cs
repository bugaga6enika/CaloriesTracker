using CaloriesTracker.Domain.Abstractions.Core;
using CaloriesTracker.Domain.Abstractions.Validation;
using CaloriesTracker.Domain.Validation.Rules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CaloriesTracker.Domain.InternalAuth
{
    public class Credentials : ValueObject
    {
        private static readonly IReadOnlyCollection<IValidationRule<string>> _usernameValidationRules;
        private static readonly IReadOnlyCollection<IValidationRule<string>> _passwordValidationRules;

        static Credentials()
        {
            _usernameValidationRules = new ReadOnlyCollection<IValidationRule<string>>
            (
                new IValidationRule<string>[]
                {
                    new StringIsNotNullOrWhiteSpaceRule()
                }
            );

            _passwordValidationRules = new ReadOnlyCollection<IValidationRule<string>>
            (
                new IValidationRule<string>[]
                {
                    new StringIsNotNullOrWhiteSpaceRule(),
                    new StringMustContainDigitRule(),
                    new StringMinLengthRule(10U),
                    new StringMustContainNonAlphanumericRule(),
                    new StringMustContainUpperCaseRule(),
                    new StringMustContainLowerCaseRule(),
                    new StringMustContainUniqueCharsRule(2)
                }
            );
        }

        private Credentials(string username, string password)
        {
            var usernameValidationResults = _usernameValidationRules.Select(x => x.ApplyTo(username, nameof(Username)));

            if (usernameValidationResults.Any(x => x.State == ValidationState.Invalid))
            {
                throw new AggregateException(usernameValidationResults.Where(x => x.State == ValidationState.Invalid).Select(x => x.Exception));
            }

            var passwordValidationResults = _passwordValidationRules.Select(x => x.ApplyTo(password, nameof(Password)));

            if (passwordValidationResults.Any(x => x.State == ValidationState.Invalid))
            {
                throw new AggregateException(passwordValidationResults.Where(x => x.State == ValidationState.Invalid).Select(x => x.Exception));
            }

            Username = username;
            Password = password;
        }

        public string Username { get; }
        public string Password { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Username;
            yield return Password;
        }

        public static Credentials Create(string username, string password)
            => new Credentials(username, password);
    }
}
