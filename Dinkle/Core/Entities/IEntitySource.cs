using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Dinkle.Core.Entities
{
    public interface IEntitySource : IDisposable
    {
        public IQueryable<T> GetItems<T>() where T : IEntity;
        public Task<T> GetItemAsync<T>(Guid id, CancellationToken ct = default) where T : IEntity;
        public Task<T> GetItemAsync<T>(Expression<Func<T, bool>> expression, CancellationToken ct = default) where T : IEntity;
        public Task<IEnumerable<T>> GetItemsAsync<T>(Expression<Func<T, bool>> expression, CancellationToken ct = default) where T : IEntity;
        public Task<IEnumerable<T>> GetItemsAsync<T>(CancellationToken ct = default) where T : IEntity;
        void Add<T>(params T[] entities) where T : IEntity;
        void Update<T>(params T[] entities) where T : IEntity;
        void Delete<T>(Guid id) where T : IEntity;
    }
}