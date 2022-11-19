using Dinkle.Core.Queries;
using Dinkle.Entities;
using Dinkle.Entities.Folder;

namespace Dinkle.Application.Folders.Queries
{
    public class GetFoldersAndFilesQuery : IQuery<FolderPayload>
    {
        public GetFoldersAndFilesQuery(string id, string apiKey, EntityType type)
        {
            Id = id;
            ApiKey = apiKey;
            Type = type;
        }
        public string ApiKey {get;}
        public string Id { get; }
        public EntityType Type { get; }
    }
}