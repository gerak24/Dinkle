using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Dinkle.Application.Accounts.Commands;
using Dinkle.Application.Accounts.Utils;
using Dinkle.Core.Entities;
using Dinkle.Core.Handlers;
using Dinkle.Entities.Account;
using Dinkle.Entities.Account.Data;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Dinkle.Application.Accounts.Handlers
{
    public class RegisterAccountCommandHandler : ICommandHandler<RegisterAccountCommand, AuthorizeResponse>
    {
        private readonly IServiceProvider _service;
        private readonly IServerEntities _entities;
        private readonly IConfiguration _configuration;

        public RegisterAccountCommandHandler(IServerEntities entities, IConfiguration configuration,
            IServiceProvider service)
        {
            _entities = entities;
            _configuration = configuration;
            _service = service;
        }

        public async Task<AuthorizeResponse> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
        {
            var messages = new List<string>();
            var isComplete = true;
            var hash = Hasher.Create(request.Password, _configuration["securityHash"]);
            var login = request.Login.ToUpperInvariant();
            var account = new Account(Guid.NewGuid(), login, hash, request.Role);
            var items = await _entities.GetItemsAsync<Account>(x => x.Login == login, cancellationToken);

            if (items.Any())
            {
                messages.Add("UserAlreadyRegister");
                isComplete = false;
            }

            if (isComplete)
            {
                _entities.Add(account);
                /*    var basepath = _configuration.GetSection("EmailOptions")["BasePath"];
                    var model = new MailMessageDto($"{basepath}", account.Login);
                    var body = await ViewToStringRenderer.RenderViewToStringAsync(_service ,$"~/Application/Main/MessageViews/RegistrationView.cshtml", model);
                    var mail = new SendMailCommand(new MailAddress(account.Login), "Register confirmed",body, _configuration);
                    await mail.SendEmailAsync();*/
            }

            return new AuthorizeResponse(null, isComplete, null, messages);
        }
    }
}