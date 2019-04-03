using MediatR;
using System;

namespace CaloriesTracker.Application.User.RegistrationSteps.DateOfBirth
{
    public class GetCurrentDateOfBirthQuery : IRequest<DateTimeOffset>
    {
    }
}
