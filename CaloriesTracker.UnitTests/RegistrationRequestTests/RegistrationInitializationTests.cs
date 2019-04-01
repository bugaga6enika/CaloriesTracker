using CaloriesTracker.Domain.Abstractions.Validation;
using CaloriesTracker.Domain.InternalAuth;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Globalization;

namespace CaloriesTracker.UnitTests.RegistrationRequestTests
{
    public class RegistrationInitializationTests
    {
        [TestCase("vtomazov@gmail.com", GoalType.GainWeight, Gender.Male, 72, 80, 182, "20/06/1984")]
        public void RegistrationRequest_Shoul_Be_Valid(string email, GoalType goal, Gender gender, int currentWeight, int? targetWeight, int heigth, string dateOfBirth)
        {
            var registrationRequest = RegistrationRequest.Create(email, goal, gender, currentWeight, targetWeight, heigth, DateTimeOffset.ParseExact(dateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture));

            registrationRequest.Email.Should().Be(email);
        }

        [TestCase("vtomazov@gmail.com", GoalType.GainWeight, Gender.Male, 72, null, 182, "20/06/1984")]
        [TestCase("vtomazov@gmail.com", GoalType.GainWeight, Gender.Male, 72, 70, 182, "20/06/1984")]
        [TestCase("vtomazov@gmail.com", GoalType.GainWeight, Gender.Male, 72, -1, 182, "20/06/1984")]
        [TestCase("vtomazov@gmail.com.net", GoalType.GainWeight, Gender.Male, 72, -1, 182, "20/06/1984")]
        public void RegistrationRequest_Shoul_Throw(string email, GoalType goal, Gender gender, int currentWeight, int? targetWeight, int heigth, string dateOfBirth)
        {
            Action registrationInit = () => RegistrationRequest.Create(email, goal, gender, currentWeight, targetWeight, heigth, DateTimeOffset.ParseExact(dateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture));

            registrationInit.Should().Throw<ValidationException>();
        }
    }
}
