namespace Dinkle.Entities.Account.Data
{
    public class ApiKey
    {
        public ApiKey(string description, string key)
        {
            Description = description;
            Key = key;
        }

        public string Description { get; }
        public string Key { get; }
    }
}