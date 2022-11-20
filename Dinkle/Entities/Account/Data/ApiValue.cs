namespace Dinkle.Entities.Account.Data
{
    public class ApiValue
    {
        public ApiValue(string description, string value)
        {
            Description = description;
            Value = value;
        }

        public string Description { get; }
        public string Value { get; }
    }
}