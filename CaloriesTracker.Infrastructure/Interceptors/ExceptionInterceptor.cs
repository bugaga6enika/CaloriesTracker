using Castle.DynamicProxy;

namespace CaloriesTracker.Infrastructure.Interceptors
{
    public class ExceptionInterceptor : IInterceptor
    {
        IAsyncInterceptor _asyncInterceptor;

        public ExceptionInterceptor(IAsyncInterceptor asyncInterceptor)
        {
            _asyncInterceptor = asyncInterceptor;
        }

        public void Intercept(IInvocation invocation)
        {
            _asyncInterceptor.ToInterceptor().Intercept(invocation);
        }
    }
}
