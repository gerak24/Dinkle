using Dinkle.Core.Queries;
using Dinkle.Entities;
using Dinkle.Entities.Folder;

namespace Dinkle.Application.Folders.Queries
{
    public class GetRootFolderQuery : IQuery<Folder>
    {
        public GetRootFolderQuery(string apiKey, EntityType type)
        {
            ApiKey = apiKey;
            Type = type;
        }

        public string ApiKey {get;}
        public EntityType Type { get; }
        
    }
}