using CaloriesTracker.Infrastructure.Rest;
using ModernHttpClient;
using System.Net.Http;

namespace CaloriesTracker.Droid.Services
{
    internal class NativeHttpClientService : INativeHttpClientService
    {
        public HttpClient Get()
        {
            return new HttpClient(new NativeMessageHandler());
        }
    }
}