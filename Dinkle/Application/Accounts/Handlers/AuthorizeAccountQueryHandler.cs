using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Dinkle.Application.Accounts.Queries;
using Dinkle.Application.Accounts.Utils;
using Dinkle.Core.Entities;
using Dinkle.Core.Handlers;
using Dinkle.Entities.Account;
using Dinkle.Entities.Account.Data;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Dinkle.Application.Accounts.Handlers
{
    public class AuthorizeAccountQueryHandler : IQueryHandler<AuthorizeAccountQuery, AuthorizeResponse>
    {
        private static readonly string AuthAccountKey = "auth_account_";

        private readonly IServerEntities _entities;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;

        public AuthorizeAccountQueryHandler(IServerEntities entities, IConfiguration configuration, IMemoryCache cache)
        {
            _entities = entities;
            _configuration = configuration;
            _cache = cache;
        }

        public async Task<AuthorizeResponse> Handle(AuthorizeAccountQuery request, CancellationToken cancellationToken)
        {
            var messages = new List<string>();
            var isComplete = true;
            var email = request.Login.ToUpperInvariant();
            var token = string.Empty;
            var accountEntity = _entities.GetItemAsync<Account>(x => x.Login == email, cancellationToken);
            if (await accountEntity == null)
            {
                messages.Add("LoginNotFound");
                return new AuthorizeResponse(token, false, null, messages);
            }

            var account = await _cache.GetOrCreateAsync($"{AuthAccountKey}_{email}", async _ => await accountEntity);

            if (!Hasher.Validate(request.Password, _configuration["securityHash"], account.Hash))
            {
                isComplete = false;
                messages.Add("PasswordsMismatch");
            }

            if (isComplete)
            {
                token = GetToken(account);
            }

            return new AuthorizeResponse(token, isComplete,Enum.GetName(account.Role) , messages);
        }

        private string GetToken(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            var claims = new[]
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, account.Id.ToString())
            };
            var identity = new ClaimsIdentity(claims, "Token");
            var now = DateTime.UtcNow;
            var sCred = new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                signingCredentials: sCred);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}