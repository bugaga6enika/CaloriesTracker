namespace CaloriesTracker.Infrastructure.Configuration
{
    public static class Configurator
    {
        public static void Init(string appName, string appVersion, string platform)
        {
            Settings.AppName = appName;
            Settings.Version = appVersion;
            Settings.Platform = platform;

            Akavache.Registrations.Start(appName);

            //Mapper.Initialize(cfg =>
            //{
            //    cfg.AddProfile<BookingProfiles>();
            //});
        }
    }
}
