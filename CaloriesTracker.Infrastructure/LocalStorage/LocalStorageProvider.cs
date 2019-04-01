using Akavache;

namespace CaloriesTracker.Infrastructure.LocalStorage
{
    internal static class LocalStorageProvider
    {
        public static IBlobCache Current => BlobCache.LocalMachine;
    }
}
