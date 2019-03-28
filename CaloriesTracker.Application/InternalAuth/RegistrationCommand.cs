using CaloriesTracker.Domain.Abstractions.Core;
using MediatR;

namespace CaloriesTracker.Application.InternalAuth
{
    public class RegistrationCommand : IRequest<OperationResult>
    {
        public string Email { get; set; }
    }
}