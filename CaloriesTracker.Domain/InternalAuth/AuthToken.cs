using CaloriesTracker.Domain.Abstractions.Core;
using CaloriesTracker.Domain.Abstractions.Validation;
using CaloriesTracker.Domain.Validation.Rules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CaloriesTracker.Domain.InternalAuth
{
    public sealed class AuthToken : ValueObject
    {
        private static readonly IReadOnlyCollection<IValidationRule<string>> _accessTokenValidationRules;
        private static readonly IReadOnlyCollection<IValidationRule<string>> _refreshTokenValidationRules;
        private static readonly IReadOnlyCollection<IValidationRule<string>> _tokenTypeValidationRules;
        private static readonly IReadOnlyCollection<IValidationRule<DateTimeOffset>> _expiresInValidationRules;

        static AuthToken()
        {
            _accessTokenValidationRules = new ReadOnlyCollection<IValidationRule<string>>
            (
                new IValidationRule<string>[]
                {
                     new StringIsNotNullOrWhiteSpaceRule()
                }
            );

            _refreshTokenValidationRules = new ReadOnlyCollection<IValidationRule<string>>
            (
                new IValidationRule<string>[]
                {
                     new StringIsNotNullOrWhiteSpaceRule()
                }
            );

            _tokenTypeValidationRules = new ReadOnlyCollection<IValidationRule<string>>
            (
                new IValidationRule<string>[]
                {
                     new StringIsNotNullOrWhiteSpaceRule()
                }
            );

            _expiresInValidationRules = new ReadOnlyCollection<IValidationRule<DateTimeOffset>>
            (
                new IValidationRule<DateTimeOffset>[]
                {
                    new DateTimeOffsetMustBeAfterNowRule()
                }
            );
        }

        private AuthToken()
        {
            AccessToken = string.Empty;
            RefreshToken = string.Empty;
            TokenType = "InvalidToken";
            ExpiresIn = DateTimeOffset.Now.AddYears(-20);
        }

        private AuthToken(string accessToken, string refreshToken, DateTimeOffset expiresIn) : this(accessToken, refreshToken, "Bearer", expiresIn)
        {
        }

        private AuthToken(string accessToken, string refreshToken, string tokenType, DateTimeOffset expiresIn)
        {
            var accessTokenValidationResults = _accessTokenValidationRules.Select(x => x.ApplyTo(accessToken, nameof(AccessToken)));

            if (accessTokenValidationResults.Any(x => x.State == ValidationState.Invalid))
            {
                throw new AggregateException(accessTokenValidationResults.Where(x => x.State == ValidationState.Invalid).Select(x => x.Exception));
            }

            var refreshTokenValidationResults = _refreshTokenValidationRules.Select(x => x.ApplyTo(refreshToken, nameof(RefreshToken)));

            if (refreshTokenValidationResults.Any(x => x.State == ValidationState.Invalid))
            {
                throw new AggregateException(refreshTokenValidationResults.Where(x => x.State == ValidationState.Invalid).Select(x => x.Exception));
            }

            var expiresInValidationResults = _expiresInValidationRules.Select(x => x.ApplyTo(expiresIn, nameof(ExpiresIn)));

            if (expiresInValidationResults.Any(x => x.State == ValidationState.Invalid))
            {
                throw new AggregateException(expiresInValidationResults.Where(x => x.State == ValidationState.Invalid).Select(x => x.Exception));
            }

            AccessToken = accessToken;
            RefreshToken = refreshToken;
            TokenType = tokenType;
            ExpiresIn = expiresIn;
        }

        public string AccessToken { get; }
        public string RefreshToken { get; }
        public string TokenType { get; }
        public DateTimeOffset ExpiresIn { get; }

        public bool IsValid => (ExpiresIn - DateTime.UtcNow).TotalMinutes > 0;

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return AccessToken;
            yield return RefreshToken;
            yield return TokenType;
            yield return ExpiresIn;
        }

        public static AuthToken Create(string accessToken, string refreshToken, DateTimeOffset expiresIn)
            => new AuthToken(accessToken, refreshToken, expiresIn);

        public static AuthToken Empty => new AuthToken();
    }
}
