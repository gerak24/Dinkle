#nullable enable
namespace Dinkle.Entities.Profile
{
    public class PersonalProfile : Profile
    {
        public PersonalProfile(string name, string secondName, string surname, string photo, string? post) : base(name, secondName,
            surname, photo)
        {
            Post = post;
        }
        public string? Post { get; }
    }
}