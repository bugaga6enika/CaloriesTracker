using CaloriesTracker.Domain.Abstractions.Core;
using CaloriesTracker.Domain.User;
using MediatR;
using System;

namespace CaloriesTracker.Application.User.RegistrationSteps.Credentials
{
    public class RegistrationCommand : IRequest<OperationResult>
    {
        public RegistrationCommand(string email, string password, GoalType goal, Domain.User.GenderType gender, double currentWeight, double? targetWeight, int height, DateTimeOffset dateOfBirth, WeightUnit weightUnit, HeightUnit heightUnit)
        {
            Email = email;
            Password = password;
            Goal = goal;
            Gender = gender;
            CurrentWeight = currentWeight;
            TargetWeight = targetWeight;
            Height = height;
            DateOfBirth = dateOfBirth;
            WeightUnit = weightUnit;
            HeightUnit = heightUnit;
        }

        public string Email { get; }
        public string Password { get; }
        public GoalType Goal { get; }
        public Domain.User.GenderType Gender { get; }
        public double CurrentWeight { get; }
        public double? TargetWeight { get; }
        public int Height { get; }
        public DateTimeOffset DateOfBirth { get; }
        public WeightUnit WeightUnit { get; }
        public HeightUnit HeightUnit { get; }
    }
}