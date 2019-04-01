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
        private static readonly IReadOnlyCollection<IValidationRule<GoalType>> _goalValidationRules;
        private static readonly IReadOnlyCollection<IValidationRule<Gender>> _genderValidationRules;
        private static readonly IReadOnlyCollection<IValidationRule<double>> _currentWeightValidationRules;
        private static readonly IReadOnlyCollection<IValidationRule<int>> _heightValidationRules;
        private static readonly IReadOnlyCollection<IValidationRule<DateTimeOffset>> _dateOfBirthValidationRules;

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

            _goalValidationRules = new ReadOnlyCollection<IValidationRule<GoalType>>
            (
                new IValidationRule<GoalType>[]
                {
                    new EnumMustBeGreaterThenNullRule<GoalType>()
                }
            );

            _genderValidationRules = new ReadOnlyCollection<IValidationRule<Gender>>
            (
               new IValidationRule<Gender>[]
               {
                    new EnumMustBeGreaterThenNullRule<Gender>()
               }
            );

            _currentWeightValidationRules = new ReadOnlyCollection<IValidationRule<double>>
            (
              new IValidationRule<double>[]
              {
                    new ValueMustBeGreaterThenRule<double>(0)
              }
            );

            _heightValidationRules = new ReadOnlyCollection<IValidationRule<int>>
            (
              new IValidationRule<int>[]
              {
                    new ValueMustBeGreaterThenRule<int>(0)
              }
            );

            _dateOfBirthValidationRules = new ReadOnlyCollection<IValidationRule<DateTimeOffset>>
            (
              new IValidationRule<DateTimeOffset>[]
              {
                    new DateTimeOffsetMustBeBeforeNowRule()
              }
            );
        }

        public string Email { get; }
        public string Password { get; }
        public GoalType Goal { get; }
        public Gender Gender { get; }
        public double CurrentWeight { get; }
        public double? TargetWeight { get; }
        public int Height { get; }
        public DateTimeOffset DateOfBirth { get; }

        private IReadOnlyCollection<IValidationRule<double?>> _targetWeightValidationRules;

        private RegistrationRequest(string email, GoalType currentGoal, Gender gender, double currentWeight, double? targetWeight, int heigth, DateTimeOffset dateOfBirth)
        {
            Validate(email, currentGoal, gender, currentWeight, targetWeight, heigth, dateOfBirth);

            Email = email;
            Goal = currentGoal;
            CurrentWeight = currentWeight;
            Height = heigth;
            DateOfBirth = dateOfBirth;
        }

        private void Validate(string email, GoalType currentGoal, Gender gender, double currentWeight, double? targetWeight, int heigth, DateTimeOffset dateOfBirth)
        {
            var emailValidationResults = _emailValidationRules.Select(x => x.ApplyTo(email, nameof(Email)));

            if (emailValidationResults.Any(x => x.State == ValidationState.Invalid))
            {
                throw new AggregateException(emailValidationResults.Where(x => x.State == ValidationState.Invalid).Select(x => x.Exception));
            }

            var goalValidationResults = _goalValidationRules.Select(x => x.ApplyTo(currentGoal, nameof(Goal)));

            if (goalValidationResults.Any(x => x.State == ValidationState.Invalid))
            {
                throw new AggregateException(goalValidationResults.Where(x => x.State == ValidationState.Invalid).Select(x => x.Exception));
            }

            if (currentGoal == GoalType.GainWeight)
            {
                _targetWeightValidationRules = new ReadOnlyCollection<IValidationRule<double?>>(new IValidationRule<double?>[]
                {
                    new ValueMustBeGreaterThenRule<double?>(0),
                    new ValueMustBeGreaterThenRule<double?>(currentWeight)
                });
            }
            else if (currentGoal == GoalType.LooseWeight)
            {
                _targetWeightValidationRules = new ReadOnlyCollection<IValidationRule<double?>>(new IValidationRule<double?>[]
                {
                    new ValueMustBeGreaterThenRule<double?>(0),
                    new ValueMustBeLessThenRule<double?>(currentWeight)
                });
            }

            if (_targetWeightValidationRules != null)
            {
                var targetWeightValidationResults = _targetWeightValidationRules.Select(x => x.ApplyTo(targetWeight, nameof(TargetWeight)));

                if (targetWeightValidationResults.Any(x => x.State == ValidationState.Invalid))
                {
                    throw new AggregateException(targetWeightValidationResults.Where(x => x.State == ValidationState.Invalid).Select(x => x.Exception));
                }
            }

            var genderValidationResults = _genderValidationRules.Select(x => x.ApplyTo(gender, nameof(Gender)));

            if (genderValidationResults.Any(x => x.State == ValidationState.Invalid))
            {
                throw new AggregateException(genderValidationResults.Where(x => x.State == ValidationState.Invalid).Select(x => x.Exception));
            }

            var weightValidationResults = _currentWeightValidationRules.Select(x => x.ApplyTo(currentWeight, nameof(CurrentWeight)));

            if (weightValidationResults.Any(x => x.State == ValidationState.Invalid))
            {
                throw new AggregateException(weightValidationResults.Where(x => x.State == ValidationState.Invalid).Select(x => x.Exception));
            }

            var heightValidationResults = _heightValidationRules.Select(x => x.ApplyTo(heigth, nameof(Height)));

            if (heightValidationResults.Any(x => x.State == ValidationState.Invalid))
            {
                throw new AggregateException(heightValidationResults.Where(x => x.State == ValidationState.Invalid).Select(x => x.Exception));
            }

            var dateOfBirthValidationResults = _dateOfBirthValidationRules.Select(x => x.ApplyTo(dateOfBirth, nameof(DateOfBirth)));

            if (dateOfBirthValidationResults.Any(x => x.State == ValidationState.Invalid))
            {
                throw new AggregateException(dateOfBirthValidationResults.Where(x => x.State == ValidationState.Invalid).Select(x => x.Exception));
            }
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Email;
            yield return Goal;
            yield return Gender;
            yield return CurrentWeight;
            yield return Height;
            yield return DateOfBirth;
        }

        public static RegistrationRequest Create(string email, GoalType goal, Gender gender, double currentWeight, double? targetWeight, int heigth, DateTimeOffset dateOfBirth)
            => new RegistrationRequest(email, goal, gender, currentWeight, targetWeight, heigth, dateOfBirth);
    }
}
