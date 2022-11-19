using Dinkle.Core.Commands;
using Dinkle.Entities.Account.Data;

namespace Dinkle.Application.Accounts.Commands
{
    public class RegisterAccountCommand : ICommand<AuthorizeResponse>
    {
        public RegisterAccountCommand(string login, string apiKey, string password)
        {
            Login = login;
            ApiKey = apiKey;
            Password = password;
        }

        public string Login { get; }
        public string ApiKey { get; }
        public string Password { get; }
    }
}