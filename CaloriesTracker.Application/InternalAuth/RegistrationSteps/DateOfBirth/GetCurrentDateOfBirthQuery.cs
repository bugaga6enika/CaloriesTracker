using MediatR;
using System;

namespace CaloriesTracker.Application.InternalAuth.RegistrationSteps.DateOfBirth
{
    public class GetCurrentDateOfBirthQuery : IRequest<DateTimeOffset>
    {
    }
}
