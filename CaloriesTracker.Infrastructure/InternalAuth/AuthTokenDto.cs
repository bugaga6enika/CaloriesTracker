using System;

namespace CaloriesTracker.Infrastructure.InternalAuth
{
    public class AuthTokenDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
        public DateTimeOffset ExpiresIn { get; set; }
    }
}
