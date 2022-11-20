#nullable enable
using System.Collections.Generic;
using Dinkle.Core.Commands;
using Dinkle.Entities;

namespace Dinkle.Application.Folders.Commands
{
    public class CreateEntityCommand : ICommand<EntityInfo>
    {
        public CreateEntityCommand(string folderId, string apiKey, string name, string? content, IEnumerable<string>? tags, bool isFolder, EntityType type)
        {
            FolderId = folderId;
            ApiKey = apiKey;
            Name = name;
            Content = content;
            Tags = tags;
            IsFolder = isFolder;
            Type = type;
        }

        public string FolderId { get; }
        public string ApiKey { get; }
        public string Name { get; }
        public string? Content { get; }
        public IEnumerable<string>? Tags { get; }
        public bool IsFolder { get; }
        public EntityType Type { get; }
    }
}