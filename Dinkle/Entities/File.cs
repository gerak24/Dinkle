#nullable enable
using System.Collections.Generic;

namespace Dinkle.Entities
{
    public class File
    {
        public File(string name, string? subscriptionId, string? parentId, string id, string type, IEnumerable<string> tags)
        {
            Name = name;
            SubscriptionId = subscriptionId;
            ParentId = parentId;
            Id = id;
            Type = type;
            Tags = tags;
        }

        public string Name { get; }
        public string Type { get; }
        public IEnumerable<string> Tags { get; }
        public string? SubscriptionId { get; }
        public string? ParentId { get; }
        public string Id { get; }
    }
}