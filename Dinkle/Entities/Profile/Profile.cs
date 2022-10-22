#nullable enable
namespace Dinkle.Entities.Profile
{
    public abstract class Profile : Entity
    {
        public Profile(string name, string secondName, string surname, string? photo)
        {
            Name = name;
            SecondName = secondName;
            Surname = surname;
            Photo = photo;
        }

        public string Name { get; }
        public string SecondName { get; }
        public string Surname { get; }
        public string? Photo { get; }
    }
}