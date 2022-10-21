using System.Threading;
using System.Threading.Tasks;
using Dinkle.Core.Buses;
using Dinkle.Core.Commands;
using MediatR;

namespace Dinkle.Infrastructure.Buses
{
    public class MediatrCommandBus : ICommandBus
    {
        private readonly IMediator _mediator;

        public MediatrCommandBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task Publish(ICommand command, CancellationToken ct = default) => _mediator.Send(command, ct);

        public Task<TResult> Send<TResult>(ICommand<TResult> command, CancellationToken ct = default) =>
            _mediator.Send(command, ct);
    }
}