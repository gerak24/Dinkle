using System.Linq;
using Dinkle.Core.Entities;
using Marten;

namespace Dinkle.Infrastructure.Entities
{
    public class MartenServerEntitiesSource : EntitySourceBase, IServerEntities
    {
        private readonly IDocumentSession _session;

        public MartenServerEntitiesSource(IDocumentSession session) : base(session)
        {
            _session = session;
        }

        protected override IQueryable<T> GetSource<T>() => _session.Query<T>().Where(x => x.AnyTenant());
    }
}