using Dinkle.Core.Commands;

namespace Dinkle.Application.ApiKey.Commanda
{
    public class AddApiKeyCommand : ICommand<Entities.Account.Data.ApiKey>
    {
        public AddApiKeyCommand(string description, string apiKey)
        {
            Description = description;
            ApiKey = apiKey;
        }

        public string ApiKey { get; }
        public string Description { get; }
    }
}