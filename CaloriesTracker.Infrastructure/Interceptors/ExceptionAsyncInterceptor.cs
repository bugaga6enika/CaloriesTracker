using CaloriesTracker.Domain.Abstractions.Rest.Exceptions;
using Refit;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace CaloriesTracker.Infrastructure.Interceptors
{
    public class ExceptionAsyncInterceptor : AsyncInterceptor
    {
        protected override async Task<T> InterceptAsync<T>(object target, MethodInfo targetMethod, IEnumerable<object> arguments, Func<Task<T>> proceed)
        {
            try
            {
                var value = await proceed();
                return value;
            }
            catch (Exception e)
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
}
