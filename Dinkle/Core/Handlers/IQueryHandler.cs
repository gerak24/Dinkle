using Dinkle.Core.Queries;
using MediatR;

namespace Dinkle.Core.Handlers
{
    public interface IQueryHandler<in TRequest, TResult> : IRequestHandler<TRequest, TResult>
        where TRequest : IQuery<TResult>
    {
    }
}