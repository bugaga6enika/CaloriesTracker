using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Application.Pipelines
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private IValidator<TRequest>[] _validators;

        public ValidationPipelineBehavior(IValidator<TRequest>[] validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var errors = new List<ValidationFailure>();
            if (_validators != null && _validators.Any())
            {
                errors = _validators.Select(v => v.Validate<TRequest>(request))
                    .Where(v => !v.IsValid)
                    .SelectMany(v => v.Errors)
                    .ToList();
            }

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }

            return await next();
        }
    }
}
