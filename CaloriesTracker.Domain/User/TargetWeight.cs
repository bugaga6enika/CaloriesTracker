using CaloriesTracker.Domain.Abstractions.Core;
using CaloriesTracker.Domain.Abstractions.Validation;
using CaloriesTracker.Domain.Validation.Rules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CaloriesTracker.Domain.User
{
    public sealed class TargetWeight : ValueObject
    {
        private IReadOnlyCollection<IValidationRule<double>> _targetWeightValidationRules;

        private TargetWeight(Weight value, GoalType goal, Weight currentWeight)
        {
            _targetWeightValidationRules = new ReadOnlyCollection<IValidationRule<double>>(new IValidationRule<double>[]
            {
                 new ValueMustBeGreaterThenIfRule<double>(() => 0, () => goal == GoalType.GainWeight || goal == GoalType.LooseWeight, "0"),
                 new ValueMustBeGreaterThenIfRule<double>(() => currentWeight.Kilograms, () => goal == GoalType.GainWeight, "Current weight"),
                 new ValueMustBeLessThenIfRule<double>(() => currentWeight.Kilograms, () => goal == GoalType.LooseWeight, "Current weight")
            });

            var targetWeightValidationResults = _targetWeightValidationRules.Select(x => x.ApplyTo(value.Kilograms, "Target weight"));

            if (targetWeightValidationResults.Any(x => x.State == ValidationState.Invalid))
            {
                throw new AggregateException(targetWeightValidationResults.Where(x => x.State == ValidationState.Invalid).Select(x => x.Exception));
            }

            Value = value;
        }

        public Weight Value { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static TargetWeight CreateForGoal(Weight value, GoalType goal, Weight currentWeight)
            => new TargetWeight(value, goal, currentWeight);

        public static implicit operator Weight(TargetWeight targetWeight) => targetWeight.Value;
    }
}
