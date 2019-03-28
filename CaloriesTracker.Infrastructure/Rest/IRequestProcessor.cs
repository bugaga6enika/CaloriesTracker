using System.Net.Http;
using System.Threading.Tasks;

namespace CaloriesTracker.Infrastructure.Rest
{
    public interface IRequestProcessor
    {
        Task<HttpRequestMessage> ProcessRequestAsync(HttpRequestMessage request);
    }
}
