using System.Threading;
using System.Threading.Tasks;
using Dinkle.Core.Buses;
using Dinkle.Core.Queries;
using MediatR;

namespace Dinkle.Infrastructure.Buses
{
    public class MediatrQueryBus : IQueryBus
    {
        private readonly IMediator _mediator;

        public MediatrQueryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<TResult> Send<TResult>(IQuery<TResult> command, CancellationToken ct = default) =>
            _mediator.Send(command, ct);
    }
}