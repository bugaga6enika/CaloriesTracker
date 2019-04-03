using CaloriesTracker.Domain.Abstractions.Validation;
using CaloriesTracker.Domain.User;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Globalization;

namespace CaloriesTracker.UnitTests.RegistrationRequestTests
{
    public class RegistrationInitializationTests
    {
        [TestCase("vtomazov@gmail.com", "superSeccre2pa$$word", GoalType.GainWeight, GenderType.Male, 72, 80, 182, "20/06/1984", WeightUnit.Kilogram, HeightUnit.Centimeter)]
        public void RegistrationRequest_Shoul_Be_Valid(string email, string password, GoalType goal, GenderType gender, int currentWeight, int? targetWeight, int heigth, string dateOfBirth, WeightUnit weightUnit, HeightUnit heightUnit)
        {
            var registrationRequest = RegistrationRequest.Create(email, password, goal, gender, currentWeight, targetWeight, heigth, DateTimeOffset.ParseExact(dateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture), weightUnit, heightUnit);

            registrationRequest.Email.Value.Should().Be(email);
        }

        [TestCase("vtomazov@gmail.com", "supersecterpasssword", GoalType.GainWeight, GenderType.Male, 72, null, 182, "20/06/1984", WeightUnit.Kilogram, HeightUnit.Centimeter)]
        [TestCase("vtomazov@gmail.com", "supersecterpasssword", GoalType.GainWeight, GenderType.Male, 72, 70, 182, "20/06/1984", WeightUnit.Kilogram, HeightUnit.Centimeter)]
        [TestCase("vtomazov@gmail.com", "supersecterpasssword", GoalType.GainWeight, GenderType.Male, 72, -1, 182, "20/06/1984", WeightUnit.Kilogram, HeightUnit.Centimeter)]
        [TestCase("vtomazov@gmail.com.net", "supersecterpasssword", GoalType.GainWeight, GenderType.Male, 72, -1, 182, "20/06/1984", WeightUnit.Kilogram, HeightUnit.Centimeter)]
        public void RegistrationRequest_Shoul_Throw(string email, string password, GoalType goal, GenderType gender, int currentWeight, int? targetWeight, int heigth, string dateOfBirth, WeightUnit weightUnit, HeightUnit heightUnit)
        {
            Action registrationInit = () => RegistrationRequest.Create(email, password, goal, gender, currentWeight, targetWeight, heigth, DateTimeOffset.ParseExact(dateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture), weightUnit, heightUnit);

            registrationInit.Should().Throw<ValidationException>();
        }
    }
}
