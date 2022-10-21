using Dinkle.Core.Queries;
using Dinkle.Entities.Account.Data;

namespace Dinkle.Application.Accounts.Queries
{
    public class AuthorizeAccountQuery : IQuery<AuthorizeResponse>
    {
        public AuthorizeAccountQuery(string password, string login)
        {
            Password = password;
            Login = login;
        }

        public string Login { get; }
        public string Password { get; }
    }
}