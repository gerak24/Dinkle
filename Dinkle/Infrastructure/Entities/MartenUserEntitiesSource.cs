using System.Linq;
using Dinkle.Core.Entities;
using Marten;

namespace Dinkle.Infrastructure.Entities
{
    public class MartenUserEntitiesSource : EntitySourceBase, IUserEntities
    {
        private readonly IDocumentSession _session;
        public MartenUserEntitiesSource(IDocumentSession session) : base(session)
        {
            _session = session;
        }

        protected override IQueryable<T> GetSource<T>() => _session.Query<T>();
    }
}