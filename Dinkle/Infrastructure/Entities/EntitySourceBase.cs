using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Dinkle.Core.Entities;
using Marten;

namespace Dinkle.Infrastructure.Entities
{
    public abstract class EntitySourceBase : IEntitySource
    {
        private readonly IDocumentSession _session;

        public EntitySourceBase(IDocumentSession session)
        {
            _session = session;
        }

        public IQueryable<T> GetItems<T>() where T : IEntity =>
            GetSource<T>();

        public Task<T> GetItemAsync<T>(Guid id, CancellationToken ct = default) where T : IEntity =>
            GetSource<T>().FirstOrDefaultAsync(x => x.Id == id, ct);

        public async Task<T> GetItemAsync<T>(Expression<Func<T, bool>> expression, CancellationToken ct = default)
            where T : IEntity =>
            await GetSource<T>().FirstOrDefaultAsync(expression, ct);

        public async Task<IEnumerable<T>> GetItemsAsync<T>(Expression<Func<T, bool>> expression,
            CancellationToken ct = default) where T : IEntity =>
            await GetSource<T>().Where(expression).ToListAsync(token: ct);

        public async Task<IEnumerable<T>> GetItemsAsync<T>(CancellationToken ct = default) where T : IEntity =>
            await GetSource<T>().ToListAsync(token: ct);

        public void Add<T>(params T[] entities) where T : IEntity =>
            _session.Insert(entities);

        public void Update<T>(params T[] entities) where T : IEntity =>
            _session.Update(entities);

        public void Delete<T>(Guid id) where T : IEntity =>
            _session.Delete<T>(id);

        public void Dispose() => _session.Dispose();
        protected abstract IQueryable<T> GetSource<T>();
    }
}