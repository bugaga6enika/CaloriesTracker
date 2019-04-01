using CaloriesTracker.Domain.Abstractions.Core;
using CaloriesTracker.Domain.InternalAuth;
using MediatR;
using System;

namespace CaloriesTracker.Application.InternalAuth.RegistrationSteps.Credentials
{
    public class RegistrationCommand : IRequest<OperationResult>
    {
        public RegistrationCommand(string email, string password, GoalType goal, Domain.InternalAuth.Gender gender, double currentWeight, double? targetWeight, int height, DateTimeOffset dateOfBirth)
        {
            Email = email;
            Password = password;
            Goal = goal;
            Gender = gender;
            CurrentWeight = currentWeight;
            TargetWeight = targetWeight;
            Height = height;
            DateOfBirth = dateOfBirth;
        }

        public string Email { get; }
        public string Password { get; }
        public GoalType Goal { get; }
        public Domain.InternalAuth.Gender Gender { get; }
        public double CurrentWeight { get; }
        public double? TargetWeight { get; }
        public int Height { get; }
        public DateTimeOffset DateOfBirth { get; }
    }
}