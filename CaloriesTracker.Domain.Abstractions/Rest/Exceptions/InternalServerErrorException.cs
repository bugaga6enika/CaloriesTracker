using System;

namespace CaloriesTracker.Domain.Abstractions.Rest.Exceptions
{
    public sealed class InternalServerErrorException : Exception
    {
        public InternalServerErrorException()
        {
        }

        public InternalServerErrorException(string message) : base(message)
        {
        }

        public InternalServerErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
