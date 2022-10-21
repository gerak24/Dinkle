using MediatR;

namespace Dinkle.Core.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
        
    }
}