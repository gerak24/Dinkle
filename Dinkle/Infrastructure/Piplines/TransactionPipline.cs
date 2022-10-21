using System;
using System.Threading;
using System.Threading.Tasks;
using Dinkle.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Dinkle.Infrastructure.Piplines
{
    public class TransactionPipline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest
        : IRequest<TResponse>
    {
        private readonly ITransactionManager _transactions;
        private readonly ILogger<ValidationPipeline<TRequest, TResponse>> _logger;

        public TransactionPipline(ITransactionManager transactions,
            ILogger<ValidationPipeline<TRequest, TResponse>> logger)
        {
            _transactions = transactions;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            try
            {
                var trx = _transactions.StartTransaction();
                var result = await next();

                if (trx)
                    await _transactions.CommitAsync(cancellationToken);

                return result;
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Error while executing transaction scope");
                throw;
            }
        }
    }
}