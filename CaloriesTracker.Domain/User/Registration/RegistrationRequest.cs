using CaloriesTracker.Domain.Abstractions.Core;
using CaloriesTracker.Domain.Abstractions.Validation;
using CaloriesTracker.Domain.Validation.Rules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CurrentWeightFactory = CaloriesTracker.Domain.User.CurrentWeight;
using DateOfBirthFactory = CaloriesTracker.Domain.User.DateOfBirth;
using TargetWeightFactory = CaloriesTracker.Domain.User.TargetWeight;

namespace CaloriesTracker.Domain.User
{
    public sealed class RegistrationRequest : ValueObject
    {
        private readonly IReadOnlyCollection<IValidationRule<GoalType>> _goalValidationRules;
        private readonly IReadOnlyCollection<IValidationRule<GenderType>> _genderValidationRules;

        private RegistrationRequest()
        {
            _goalValidationRules = new ReadOnlyCollection<IValidationRule<GoalType>>
            (
                new IValidationRule<GoalType>[]
                {
                    new EnumMustBeGreaterThenNullRule<GoalType>()
                }
            );

            _genderValidationRules = new ReadOnlyCollection<IValidationRule<GenderType>>
            (
               new IValidationRule<GenderType>[]
               {
                    new EnumMustBeGreaterThenNullRule<GenderType>()
               }
            );
        }

        public Email Email { get; }
        public Password Password { get; }
        public GoalType Goal { get; }
        public GenderType Gender { get; }
        public Weight CurrentWeight { get; }
        public Weight TargetWeight { get; }
        public Height Height { get; }
        public DateOfBirthFactory DateOfBirth { get; }

        private RegistrationRequest(string email, string password, GoalType currentGoal, GenderType gender, double currentWeight, double? targetWeight, int height, DateTimeOffset dateOfBirth, WeightUnit weightUnit, HeightUnit heightUnit) : this()
        {
            var goalValidationResults = _goalValidationRules.Select(x => x.ApplyTo(currentGoal, nameof(Goal)));

            if (goalValidationResults.Any(x => x.State == ValidationState.Invalid))
            {
                throw new AggregateException(goalValidationResults.Where(x => x.State == ValidationState.Invalid).Select(x => x.Exception));
            }

            var genderValidationResults = _genderValidationRules.Select(x => x.ApplyTo(gender, nameof(Gender)));

            if (genderValidationResults.Any(x => x.State == ValidationState.Invalid))
            {
                throw new AggregateException(genderValidationResults.Where(x => x.State == ValidationState.Invalid).Select(x => x.Exception));
            }

            Email = Email.Parse(email);
            Password = Password.Parse(password);
            Goal = currentGoal;
            Gender = gender;
            CurrentWeight = CurrentWeightFactory.CreateForGoal(Weight.Create(currentWeight, weightUnit), currentGoal, targetWeight.HasValue ? Weight.Create(targetWeight.Value, weightUnit) : Weight.Nullable);
            TargetWeight = TargetWeightFactory.CreateForGoal(targetWeight.HasValue ? Weight.Create(targetWeight.Value, weightUnit) : Weight.Nullable, currentGoal, Weight.Create(currentWeight, weightUnit));
            Height = Height.Create(height, heightUnit);
            DateOfBirth = DateOfBirthFactory.Create(dateOfBirth);
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

        public static RegistrationRequest Create(string email, string password, GoalType goal, GenderType gender, double currentWeight, double? targetWeight, int heigth, DateTimeOffset dateOfBirth, WeightUnit weightUnit, HeightUnit heightUnit)
            => new RegistrationRequest(email, password, goal, gender, currentWeight, targetWeight, heigth, dateOfBirth, weightUnit, heightUnit);
    }
}
