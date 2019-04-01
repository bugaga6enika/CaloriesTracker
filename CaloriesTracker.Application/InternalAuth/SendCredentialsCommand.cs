using CaloriesTracker.Domain.InternalAuth;
using MediatR;

namespace CaloriesTracker.Application.InternalAuth
{
    public class SendCredentialsCommand : IRequest<AuthToken>
    {
        public SendCredentialsCommand(string username, string password, string deviceId)
        {
            Username = username;
            Password = password;
            DeviceId = deviceId;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string DeviceId { get; set; }
    }
}