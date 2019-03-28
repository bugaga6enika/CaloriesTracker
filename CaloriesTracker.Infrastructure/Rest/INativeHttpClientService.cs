using System.Net.Http;

namespace CaloriesTracker.Infrastructure.Rest
{
    public interface INativeHttpClientService
    {
        HttpClient Get();
    }
}