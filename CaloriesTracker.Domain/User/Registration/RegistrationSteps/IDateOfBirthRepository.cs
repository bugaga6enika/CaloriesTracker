using System;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Domain.User.RegistrationSteps
{
    public interface IDateOfBirthRepository
    {
        Task<bool> Save(DateTimeOffset dateOfBirth, CancellationToken cancellationToken);
        Task<DateTimeOffset> GetCurrent();
    }
}