using MediatR;
using System;

namespace CaloriesTracker.Application.InternalAuth.RegistrationSteps.DateOfBirth
{
    public class SaveDateOfBirthCommand : IRequest<bool>
    {
        public SaveDateOfBirthCommand(DateTimeOffset dateOfBirth)
        {
            DateOfBirth = dateOfBirth;
        }

        public DateTimeOffset DateOfBirth { get; }
    }
}
