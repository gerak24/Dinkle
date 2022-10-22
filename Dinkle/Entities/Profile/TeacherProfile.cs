#nullable enable
namespace Dinkle.Entities.Profile
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile(string name, string secondName, string surname, string photo, string? supervisedGroup, bool isCurator, string? cafedra) : base(name, secondName,
            surname, photo)
        {
            SupervisedGroup = supervisedGroup;
            IsCurator = isCurator;
            Cafedra = cafedra;
        }
        public string? Cafedra { get; }
        public bool IsCurator { get; }
        public string? SupervisedGroup { get; }
    }
}