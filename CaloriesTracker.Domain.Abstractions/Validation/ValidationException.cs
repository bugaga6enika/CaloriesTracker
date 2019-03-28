using System;
using System.Runtime.Serialization;

namespace CaloriesTracker.Domain.Abstractions.Validation
{
    public class ValidationException : Exception
    {
        public string ParamName { get; }

        public ValidationException(string message, string paramName) : this(message, paramName, null)
        {
        }

        public ValidationException(string message, string paramName, Exception innerException) : base(message, innerException)
        {
            ParamName = paramName;
        }

        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}