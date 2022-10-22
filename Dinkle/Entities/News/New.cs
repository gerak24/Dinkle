#nullable enable
namespace Dinkle.Entities.News
{
    public class New : Entity
    {
        public New(string owner, string description, string? image, NewType type, string ownerPhoto)
        {
            Owner = owner;
            Description = description;
            Image = image;
            Type = type;
            OwnerPhoto = ownerPhoto;
        }

        public string Owner { get; }
        public string OwnerPhoto { get; }
        public string Description { get; }
        public string? Image { get; }
        public NewType Type { get; }
    }
}