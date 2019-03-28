using System;
using static CaloriesTracker.Domain.Abstractions.Validation.ValidationState;

namespace CaloriesTracker.Domain.Abstractions.Validation
{
    public sealed class ValidationResult
    {
        private ValidationResult(ValidationState state, ValidationException exception)
        {
            if (state == Invalid && exception == null)
            {
                throw new ArgumentNullException("Provide info about the validation failure");
            }

            if (state == Valid && exception != null)
            {
                throw new ArgumentException("No exception expected for Valid result");
            }

            State = state;
            Exception = exception;
        }

        public ValidationState State { get; }
        public ValidationException Exception { get; }

        public static ValidationResult ValidResult => new ValidationResult(Valid, null);
        public static ValidationResult InvalidResult(ValidationException e) => new ValidationResult(Invalid, e);
    }
}