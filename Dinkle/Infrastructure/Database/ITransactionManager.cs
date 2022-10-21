using System.Threading;
using System.Threading.Tasks;

namespace Dinkle.Infrastructure.Database
{
    public interface ITransactionManager
    {
        bool StartTransaction();

        Task CommitAsync(CancellationToken ct = default);
    }
}