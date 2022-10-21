using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Dinkle.Infrastructure.Piplines
{
    public class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest
        : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<ValidationPipeline<TRequest, TResponse>> _logger;

        public ValidationPipeline(IEnumerable<IValidator<TRequest>> validators,
            ILogger<ValidationPipeline<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            _logger.LogDebug("Try to validate request {Request}", typeof(TRequest));

            var results = new List<ValidationResult>();

            foreach (var validator in _validators)
                results.Add(await validator.ValidateAsync(request, cancellationToken));

            var errors = results
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (!errors.Any())
                return await next();

            _logger.LogWarning("Request {Request} have errors: {@Errors}",
                typeof(TRequest), errors);

            throw new ValidationException("Error validation request", errors);
        }
    }
}