using CaloriesTracker.Infrastructure.User;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace CaloriesTracker.Infrastructure.Configuration
{
    internal class Settings
    {
        private static ISettings _settings => CrossSettings.Current;

        public static string RestEndpointUrl => "http://localhost:44331";

        public static string AppName
        {
            get => _settings.GetValueOrDefault(nameof(AppName), default(string));
            set => _settings.AddOrUpdateValue(nameof(AppName), value);
        }

        public static string Version
        {
            get => _settings.GetValueOrDefault(nameof(Version), default(string));
            set => _settings.AddOrUpdateValue(nameof(Version), value);
        }

        public static string Platform
        {
            get => _settings.GetValueOrDefault(nameof(Platform), default(string));
            set => _settings.AddOrUpdateValue(nameof(Platform), value);
        }

        public static string DeviceId
        {
            get => _settings.GetValueOrDefault(nameof(DeviceId), default(string));
            set => _settings.AddOrUpdateValue(nameof(DeviceId), value);
        }

        private static string _authTokenAsJson
        {
            get => _settings.GetValueOrDefault(nameof(_authTokenAsJson), default(string));
            set => _settings.AddOrUpdateValue(nameof(_authTokenAsJson), value);
        }

        public static AuthTokenDto CurrentToken
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_authTokenAsJson))
                {
                    return JsonConvert.DeserializeObject<AuthTokenDto>(_authTokenAsJson);
                }

                return default(AuthTokenDto);
            }

            set
            {
                if (value != default(AuthTokenDto))
                {
                    _authTokenAsJson = JsonConvert.SerializeObject(value);
                }
                else
                {
                    _authTokenAsJson = default(string);
                }
            }
        }
    }
}
