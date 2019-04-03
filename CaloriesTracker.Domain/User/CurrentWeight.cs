using CaloriesTracker.Domain.Abstractions.Core;
using CaloriesTracker.Domain.Abstractions.Validation;
using CaloriesTracker.Domain.Validation.Rules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CaloriesTracker.Domain.User
{
    public sealed class CurrentWeight : ValueObject
    {
        private IReadOnlyCollection<IValidationRule<double>> _currentWeightValidationRules;

        private CurrentWeight(Weight value, GoalType goal, Weight targetWeight)
        {
            _currentWeightValidationRules = new ReadOnlyCollection<IValidationRule<double>>(new IValidationRule<double>[]
            {
                new ValueMustBeGreaterThenRule<double>(0),
                new ValueMustBeGreaterThenIfRule<double>(() => targetWeight.Kilograms, () => goal == GoalType.LooseWeight, "Target weight"),
                new ValueMustBeLessThenIfRule<double>(() => targetWeight.Kilograms, () => goal == GoalType.GainWeight, "Target weight")
            });

            var weightValidationResults = _currentWeightValidationRules.Select(x => x.ApplyTo(value.Kilograms, nameof(CurrentWeight)));

            if (weightValidationResults.Any(x => x.State == ValidationState.Invalid))
            {
                throw new AggregateException(weightValidationResults.Where(x => x.State == ValidationState.Invalid).Select(x => x.Exception));
            }

            Value = value;
        }

        public Weight Value { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static CurrentWeight CreateForGoal(Weight value, GoalType goal, Weight targetWeight)
            => new CurrentWeight(value, goal, targetWeight);

        public static implicit operator Weight(CurrentWeight currentWeight) => currentWeight.Value;
    }
}
