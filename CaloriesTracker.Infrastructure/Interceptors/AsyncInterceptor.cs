using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace CaloriesTracker.Infrastructure.Interceptors
{
    /// <summary>
    /// Code taken from  <see href="https://codearticles.ru/articles/5007?AspxAutoDetectCookieSupport=1">here</see>
    /// </summary>   
    public abstract class AsyncInterceptor : IInterceptor
    {
        private readonly MethodInfo _handleAsyncMethodInfo = typeof(AsyncInterceptor).GetMethod(nameof(HandleAsyncWithResult), BindingFlags.Instance | BindingFlags.NonPublic);
        private enum InvocationMethodResultType { Sync, AsyncAction, AsyncFunction }

        public void Intercept(IInvocation invocation)
        {
            var delegateType = GetInvocationMethodResultType(invocation);

            switch (delegateType)
            {
                case InvocationMethodResultType.Sync:
                    invocation.ReturnValue = HandleSync(invocation);
                    break;

                case InvocationMethodResultType.AsyncAction:
                    invocation.ReturnValue = HandleAsync(invocation);
                    break;

                case InvocationMethodResultType.AsyncFunction:
                    var resultType = invocation.Method.ReturnType.GetGenericArguments()[0];
                    var methodInfo = _handleAsyncMethodInfo.MakeGenericMethod(resultType);
                    invocation.ReturnValue = methodInfo.Invoke(this, new[] { invocation, invocation.ReturnValue });
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected abstract Task<T> InterceptAsync<T>(object target, MethodInfo targetMethod, IEnumerable<object> arguments, Func<Task<T>> proceed);

        private InvocationMethodResultType GetInvocationMethodResultType(IInvocation invocation)
        {
            var returnType = invocation.Method.ReturnType;
            if (returnType == typeof(Task))
                return InvocationMethodResultType.AsyncAction;
            if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
                return InvocationMethodResultType.AsyncFunction;

            return InvocationMethodResultType.Sync;
        }

        private async Task HandleAsync(IInvocation invocation)
        {
            await InterceptAsync(invocation.InvocationTarget, invocation.Method, invocation.Arguments, () => Task.Run(async () =>
            {
                try
                {
                    await (Task)invocation.Method.Invoke(invocation.InvocationTarget, invocation.Arguments);
                }
                catch (TargetInvocationException e) when (e.InnerException != null)
                {
                    ExceptionDispatchInfo.Capture(e.InnerException).Throw();
                }

                return new object();
            }));
        }

        private async Task<T> HandleAsyncWithResult<T>(IInvocation invocation, Task<T> returnValue)
        {
            return await InterceptAsync(invocation.InvocationTarget, invocation.Method, invocation.Arguments, async () => await Task.Run(async () =>
            {
                try
                {
                    return await (Task<T>)invocation.Method.Invoke(invocation.InvocationTarget, invocation.Arguments);
                }
                catch (TargetInvocationException e) when (e.InnerException != null)
                {
                    ExceptionDispatchInfo.Capture(e.InnerException).Throw();
                    throw;
                }
            }));
        }

        private object HandleSync(IInvocation invocation)
        {
            try
            {
                InterceptAsync(invocation.InvocationTarget, invocation.Method, invocation.Arguments, () => Task.Run(() =>
                {
                    try
                    {
                        invocation.Proceed();
                    }
                    catch (TargetInvocationException e) when (e.InnerException != null)
                    {
                        return Task.FromException<object>(e.InnerException);
                    }

                    return Task.FromResult(new object());
                })).Wait();
            }
            catch (AggregateException e) when (e.InnerException != null)
            {
                ExceptionDispatchInfo.Capture(e.InnerException).Throw();
            }

            return invocation.ReturnValue;
        }
    }
}
