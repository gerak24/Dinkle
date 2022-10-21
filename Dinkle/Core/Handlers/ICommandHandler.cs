using Dinkle.Core.Commands;
using MediatR;

namespace Dinkle.Core.Handlers
{
    public interface ICommandHandler<in TRequest> : IRequestHandler<TRequest> where TRequest : ICommand
    {
    }

    public interface ICommandHandler<in TRequest, TResult> : IRequestHandler<TRequest, TResult>
        where TRequest : ICommand<TResult>
    {
    }
}