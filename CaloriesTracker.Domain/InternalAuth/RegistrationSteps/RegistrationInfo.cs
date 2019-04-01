using System;

namespace CaloriesTracker.Domain.InternalAuth.RegistrationSteps
{
    public class RegistrationInfo
    {
        public GoalType Goal { get; set; }
        public Gender Gender { get; set; }
        public double CurrentWeight { get; set; }
        public double? TargetWeight { get; set; }
        public int Height { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
    }
}
