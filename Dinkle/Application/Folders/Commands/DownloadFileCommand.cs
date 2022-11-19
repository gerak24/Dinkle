using Dinkle.Core.Commands;
using Dinkle.Entities;

namespace Dinkle.Application.Folders.Commands
{
    public class DownloadFileCommand : ICommand<string>
    {
        public DownloadFileCommand(string apiKey, string id, EntityType type)
        {
            ApiKey = apiKey;
            Id = id;
            Type = type;
        }

        public string ApiKey { get; }
        public string Id { get; }
        public EntityType Type { get; }
    }
}