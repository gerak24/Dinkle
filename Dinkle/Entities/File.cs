#nullable enable
namespace Dinkle.Entities
{
    public class File
    {
        public File(string name, string? subscriptionId, string? parentId, string id)
        {
            Name = name;
            SubscriptionId = subscriptionId;
            ParentId = parentId;
            Id = id;
        }

        public string Name { get; }
        public string? SubscriptionId { get; }
        public string? ParentId { get; }
        public string Id { get; }
    }
}