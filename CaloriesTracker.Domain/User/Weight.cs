using CaloriesTracker.Domain.Abstractions.Core;
using CaloriesTracker.Domain.Abstractions.Validation;
using CaloriesTracker.Domain.Validation.Rules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CaloriesTracker.Domain.User
{
    public sealed class Weight : ValueObject
    {
        private readonly IReadOnlyCollection<IValidationRule<double>> _weightValidationRules;
        private const double _kgToPoundRatio = 2.20462;

        public Weight()
        {
            _weightValidationRules = new ReadOnlyCollection<IValidationRule<double>>(new IValidationRule<double>[]
            {
                new ValueMustBeGreaterThenRule<double>(0),
                new ValueMustBeLessThenRule<double>(500)
            });
        }

        private Weight(double value, WeightUnit weightUnit) : this()
        {
            var kilograms = weightUnit == WeightUnit.Kilogram ? value : ConvertToKilograms(value);

            var weightValidationResults = _weightValidationRules.Select(x => x.ApplyTo(kilograms, nameof(Weight)));

            if (weightValidationResults.Any(x => x.State == ValidationState.Invalid))
            {
                throw new AggregateException(weightValidationResults.Where(x => x.State == ValidationState.Invalid).Select(x => x.Exception));
            }

            Kilograms = kilograms;
            Pounds = weightUnit == WeightUnit.Pound ? value : ConvertToPounds(value);
        }

        public double Kilograms { get; }
        public double Pounds { get; }

        private double ConvertToKilograms(double pounds)
            => pounds / _kgToPoundRatio;

        private double ConvertToPounds(double kilograms)
            => kilograms * _kgToPoundRatio;

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Kilograms;
            yield return Pounds;
        }

        public static Weight Create(double value, WeightUnit weightUnit)
            => new Weight(value, weightUnit);

        public static Weight SetKilograms(double kilograms)
            => new Weight(kilograms, WeightUnit.Kilogram);

        public static Weight SetPounds(double pounds)
            => new Weight(pounds, WeightUnit.Pound);

        public static Weight Nullable => new Weight();
    }
}
