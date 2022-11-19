#nullable enable
namespace Dinkle.Entities.Folder
{
    public class Folder
    {
        public Folder(string name, string subscriptionId, string id)
        {
            Name = name;
            SubscriptionId = subscriptionId;
            Id = id;
        }
        public string Name { get; }
        public string SubscriptionId { get; }
        public string Id { get; }
    }
}
