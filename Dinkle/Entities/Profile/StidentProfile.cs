#nullable enable
namespace Dinkle.Entities.Profile
{
    public class StidentProfile : Profile
    {
        public StidentProfile(string name, string secondName, string surname, string photo, string? course, string? faculty, string? group, string? number) : base(name, secondName,
            surname, photo)
        {
            Course = course;
            Faculty = faculty;
            Group = group;
            Number = number;
        }
        public string? Number { get; }
        public string? Group { get; }
        public string? Faculty { get; }
        public string? Course { get; }
    }
}