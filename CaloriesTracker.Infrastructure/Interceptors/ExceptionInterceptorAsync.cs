using CaloriesTracker.Domain.Abstractions.Rest.Exceptions;
using Castle.DynamicProxy;
using Refit;
using System;
using System.Threading.Tasks;

namespace CaloriesTracker.Infrastructure.Interceptors
{
    public class ExceptionInterceptorAsync : IAsyncInterceptor
    {
        public void InterceptAsynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous(invocation);
        }

        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous<TResult>(invocation);
        }

        public void InterceptSynchronous(IInvocation invocation)
        {
            string methodName = "";
            object[] methodArguments = new object[] { };

            try
            {
                methodName = invocation.GetConcreteMethod().Name;
                methodArguments = invocation.Arguments;

                invocation.Proceed();
            }
            catch (Exception e)
            {
                ThrowCorrespondingException(e);
            }
        }

        private async Task InternalInterceptAsynchronous(IInvocation invocation)
        {
            string methodName = "";
            object[] methodArguments = new object[] { };

            try
            {
                methodName = invocation.GetConcreteMethod().Name;
                methodArguments = invocation.Arguments;

                invocation.Proceed();

                var task = (Task)invocation.ReturnValue;
                await task;
            }
            catch (Exception e)
            {
                ThrowCorrespondingException(e);
            }
        }

        private async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation)
        {
            string methodName = "";
            object[] methodArguments = new object[] { };

            try
            {
                methodName = invocation.GetConcreteMethod().Name;
                methodArguments = invocation.Arguments;

                invocation.Proceed();

                var task = (Task<TResult>)invocation.ReturnValue;
                TResult result = await task;

                return result;
            }
            catch (Exception e)
            {
                ThrowCorrespondingException(e);
            }

            return default(TResult);
        }

        private void ThrowCorrespondingException(Exception e)
        {
            if (e is ApiException exception)
            {
                switch (exception.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized:
                        {
                            throw new UnauthorizedException(e.Message, e);
                        }
                    case System.Net.HttpStatusCode.Forbidden:
                        {
                            throw new ForbiddenException(e.Message, e);
                        }
                    case System.Net.HttpStatusCode.InternalServerError:
                        {
                            throw new InternalServerErrorException(e.Message, e);
                        }
                    case System.Net.HttpStatusCode.BadRequest:
                        {
                            throw new BadRequestException(e.Message, e);
                        }
                    case System.Net.HttpStatusCode.NotFound:
                        {
                            throw new NotFoundException(e.Message, e);
                        }
                    default:
                        {
                            throw new InternalServerErrorException(e.Message, e);
                        }
                }
            }

            throw e;
        }
    }
}
