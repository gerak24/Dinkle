using Dinkle.Core.Commands;
using Dinkle.Entities.Account.Data;

namespace Dinkle.Application.Accounts.Commands
{
    public class RegisterAccountCommand : ICommand<AuthorizeResponse>
    {
        public RegisterAccountCommand(string login, string password, UserRole role)
        {
            Login = login;
            Password = password;
            Role = role;
        }

        public string Login { get; }
        public string Password { get; }
        public UserRole Role { get; }
    }
}