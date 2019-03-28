using CaloriesTracker.Infrastructure.Rest;
using ModernHttpClient;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Droid.Services
{
    internal class HttpClientMiddleware : NativeMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request = await CaloriesTracker.Application.Configuration.IoC.ServiceLocator.Current.Resolve<IRequestProcessor>().ProcessRequestAsync(request);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}