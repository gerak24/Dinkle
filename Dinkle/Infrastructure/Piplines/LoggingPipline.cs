using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Dinkle.Infrastructure.Piplines
{
    public class LoggingPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest
        : IRequest<TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            return next();
        }
    }
}