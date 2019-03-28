using CaloriesTracker.Domain.InternalAuth;
using MediatR;

namespace CaloriesTracker.Application.InternalAuth
{
    public class SendCredentialsCommand : IRequest<AuthToken>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string DeviceId { get; set; }
    }
}