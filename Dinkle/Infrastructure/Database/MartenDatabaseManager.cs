using System;
using System.Threading;
using System.Threading.Tasks;
using Dinkle.Core.Entities;
using Dinkle.Infrastructure.Entities;
using Marten;

namespace Dinkle.Infrastructure.Database
{
    public class MartenDatabaseManager : IDisposable, ITransactionManager, ISourceManager
    {
        private readonly IDocumentStore _store;
        private IDocumentSession _session;
        private bool _isTransactionStarted;

        public MartenDatabaseManager(IDocumentStore store)
        {
            _store = store;
        }

        public IUserEntities GetUserEntities(Guid tenantId) =>
            new MartenUserEntitiesSource(_session ??= _store.OpenSession(tenantId.ToString()));

        public IServerEntities GetServerEntities() =>
            new MartenServerEntitiesSource(_session ??= _store.OpenSession());

        public bool StartTransaction()
        {
            switch (_isTransactionStarted)
            {
                case true:
                    return false;
                default:
                    _isTransactionStarted = true;
                    return true;
            }
        }

        public Task CommitAsync(CancellationToken ct = default) => _session?.SaveChangesAsync(ct);


        public void Dispose()
        {
            _session?.Dispose();
            _store?.Dispose();
        }
    }
}