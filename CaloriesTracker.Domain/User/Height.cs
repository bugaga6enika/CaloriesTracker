using CaloriesTracker.Domain.Abstractions.Core;
using CaloriesTracker.Domain.Abstractions.Validation;
using CaloriesTracker.Domain.Validation.Rules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CaloriesTracker.Domain.User
{
    public sealed class Height : ValueObject
    {
        private readonly IReadOnlyCollection<IValidationRule<double>> _heightValidationRules;

        private const double _footToCentimeterRatio = 30.48;
        private const double _inchToCentimeterRatio = 2.54;

        private Height()
        {
            _heightValidationRules = new ReadOnlyCollection<IValidationRule<double>>(new IValidationRule<double>[]
            {
                new ValueMustBeGreaterThenRule<double>(0),
                new ValueMustBeLessThenRule<double>(3000)
            });
        }

        private Height(double value, HeightUnit heightUnit) : this()
        {
            var centimeters = heightUnit == HeightUnit.Centimeter ? value : ConvertToCentimeters(value);

            var heightValidationResults = _heightValidationRules.Select(x => x.ApplyTo(centimeters, nameof(Height)));

            if (heightValidationResults.Any(x => x.State == ValidationState.Invalid))
            {
                throw new AggregateException(heightValidationResults.Where(x => x.State == ValidationState.Invalid).Select(x => x.Exception));
            }

            Centimeters = centimeters;
            (Feet, Inches) = GetFeetAndInches(centimeters);
        }

        private double ConvertToCentimeters(double feetAndInches)
            => feetAndInches * _footToCentimeterRatio;

        private double ConvertToFeetAndInches(double centimeters)
            => centimeters / _footToCentimeterRatio;

        private (ushort feet, double inches) GetFeetAndInches(double centimeters)
        {
            var feetAndInces = ConvertToFeetAndInches(centimeters);
            return (feet: (ushort)feetAndInces, inches: feetAndInces - (ushort)feetAndInces);
        }

        public double Centimeters { get; }
        public ushort Feet { get; }
        public double Inches { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Centimeters;
            yield return Feet;
            yield return Inches;
        }

        public static Height Create(double value, HeightUnit heightUnit)
            => new Height(value, heightUnit);

        public static Height Nullable => new Height();
    }
}
