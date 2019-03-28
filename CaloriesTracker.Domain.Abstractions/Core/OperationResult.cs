using System;

namespace CaloriesTracker.Domain.Abstractions.Core
{
    public sealed class OperationResult
    {
        private OperationResult(OperationStatus status, Exception exception)
        {
            if (status == OperationStatus.Fail && exception == null)
            {
                throw new ArgumentNullException("Provide info about the operation failure");
            }

            if (status == OperationStatus.Success && exception != null)
            {
                throw new ArgumentException("No exception expected for Success result");
            }

            Status = status;
            Exception = exception;
        }

        public OperationStatus Status { get; }

        public Exception Exception { get; }

        public static OperationResult SuccessOperation => new OperationResult(OperationStatus.Success, null);
        public static OperationResult FailedOperation(Exception e) => new OperationResult(OperationStatus.Fail, e);
    }
}
