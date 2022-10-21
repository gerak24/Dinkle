using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dinkle.Application.Accounts.Commands;
using Dinkle.Application.Accounts.Utils;
using Dinkle.Core.Entities;
using Dinkle.Core.Handlers;
using Dinkle.Entities.Account;
using Dinkle.Entities.Account.Data;
using Microsoft.Extensions.Configuration;

namespace Dinkle.Application.Accounts.Handlers
{
    public class RecoverPasswordCommandHandler : ICommandHandler<RecoverPasswordCommand, AuthorizeResponse>
    {
        private readonly IServerEntities _entities;
        private readonly IConfiguration _configuration;

        public RecoverPasswordCommandHandler(IServerEntities entities, IConfiguration configuration)
        {
            _entities = entities;
            _configuration = configuration;
        }

        public async Task<AuthorizeResponse> Handle(RecoverPasswordCommand request, CancellationToken cancellationToken)
        {
            var messages = new List<string>();
            var isComplete = true;
            var email = request.Login.ToUpperInvariant();
            var account = await _entities.GetItemAsync<Account>(x => x.Login == email, cancellationToken);


            if (account != null)
            {
                account.Hash = Hasher.Create(request.NewPassword, _configuration["securityHash"]);
                _entities.Update(account);
            }
            else
            {
                messages.Add("LoginNotFound");
                isComplete = false;
            }

            return new AuthorizeResponse(null, isComplete, null, messages);
        }
    }
}