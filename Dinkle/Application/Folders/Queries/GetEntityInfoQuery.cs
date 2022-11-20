using Dinkle.Core.Queries;
using Dinkle.Entities;

namespace Dinkle.Application.Folders.Queries
{
    public class GetEntityInfoQuery : IQuery<EntityInfo>
    {
        public GetEntityInfoQuery(string id, string apiKey, EntityType type)
        {
            Id = id;
            ApiKey = apiKey;
            Type = type;
        }

        public string Id { get; }
        public string ApiKey { get; }
        public EntityType Type { get; }
    }
}