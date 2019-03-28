using CaloriesTracker.Domain.Abstractions.Rest.Exceptions;
using Refit;
using System;
using System.Net.Http;

namespace CaloriesTracker.Infrastructure.Rest.Repositories
{
    public abstract class RestRepositoryBase<T> where T : IRestRepository
    {
        protected T RestService;
        protected readonly HttpClient _httpClient;

        public RestRepositoryBase(HttpClient httpClient)
        {
            _httpClient = httpClient;

            ConfigureRestService();
        }

        protected virtual void ConfigureRestService()
        {
            RestService = Refit.RestService.For<T>(_httpClient);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public void ForwardException(Exception e)
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
