using Dinkle.Core.Commands;
using Dinkle.Entities.Account.Data;

namespace Dinkle.Application.Accounts.Commands
{
    public class RecoverPasswordCommand : ICommand<AuthorizeResponse>
    {
        public RecoverPasswordCommand(string login, string newPassword)
        {
            Login = login;
            NewPassword = newPassword;
        }

        public string Login { get; }
        public string NewPassword { get; }
    }
}