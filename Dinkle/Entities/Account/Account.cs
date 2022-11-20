using System;
using System.Collections.Generic;
using Dinkle.Entities.Account.Data;

namespace Dinkle.Entities.Account
{
    public class Account : Entity
    {
        public Account(Guid id,string login, string hash, IEnumerable<ApiKey> apiKey)
        {
            Id = id;
            Login = login;
            Hash = hash;
            ApiKey = apiKey;
        }
        public IEnumerable<ApiKey> ApiKey { get; }
        public string Login { get; }
        public string Hash { get; set; }
    }
}