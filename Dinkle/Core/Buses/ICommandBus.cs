using System.Threading;
using System.Threading.Tasks;
using Dinkle.Core.Commands;

namespace Dinkle.Core.Buses
{
    public interface ICommandBus
    {
        /// <summary>
        /// Send command without result
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="ct"></param>
        Task Publish(ICommand command, CancellationToken ct = default);

        /// <summary>
        /// Send command with return result
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="ct"></param>
        /// <typeparam name="TResult">Result of command execution</typeparam>
        /// <returns></returns>
        Task<TResult> Send<TResult>(ICommand<TResult> command, CancellationToken ct = default);
    }
}