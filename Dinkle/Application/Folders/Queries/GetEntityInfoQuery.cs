using Dinkle.Core.Queries;
using Dinkle.Entities;

namespace Dinkle.Application.Folders.Queries
{
    public class GetEntityInfoQuery : IQuery<EntityInfo>
    {
        public GetEntityInfoQuery(string id, string apiKey)
        {
            Id = id;
            ApiKey = apiKey;
        }

        public string Id { get; }
        public string ApiKey { get; }
    }
}