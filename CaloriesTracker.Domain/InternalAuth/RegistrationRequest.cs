using CaloriesTracker.Domain.Abstractions.Core;
using CaloriesTracker.Domain.Abstractions.Validation;
using CaloriesTracker.Domain.Validation.Rules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CaloriesTracker.Domain.InternalAuth
{
    public sealed class RegistrationRequest : ValueObject
    {
        private static readonly IReadOnlyCollection<IValidationRule<string>> _emailValidationRules;

        static RegistrationRequest()
        {
            _emailValidationRules = new ReadOnlyCollection<IValidationRule<string>>
            (
                new IValidationRule<string>[]
                {
                    new StringIsNotNullOrWhiteSpaceRule(),
                    new EmailMustBeValidRule()
                }
            );
        }

        public string Email { get; }

        private RegistrationRequest(string email)
        {
            var emailValidationResults = _emailValidationRules.Select(x => x.ApplyTo(email, nameof(Email)));

            if (emailValidationResults.Any(x => x.State == ValidationState.Invalid))
            {
                throw new AggregateException(emailValidationResults.Where(x => x.State == ValidationState.Invalid).Select(x => x.Exception));
            }

            Email = email;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Email;
        }

        public static RegistrationRequest Create(string email)
            => new RegistrationRequest(email);
    }
}
