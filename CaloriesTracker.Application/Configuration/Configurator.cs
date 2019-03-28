using CaloriesTracker.Infrastructure.Rest;
using System.Net.Http;

namespace CaloriesTracker.Application.Configuration
{
    public static class Configurator
    {
        internal static HttpClient HttpClient { get; private set; }
        internal static INativeHttpClientService NativeHttpClientService { get; private set; }

        public static void Init(HttpClient httpClient, INativeHttpClientService nativeHttpClientService)
        {
            HttpClient = httpClient;
            NativeHttpClientService = nativeHttpClientService;
        }
    }
}
