using System;
using System.Collections.Generic;

namespace Dinkle.Entities.Account
{
    public class Account : Entity
    {
        public Account(Guid id,string login, string hash, IEnumerable<string> apiKey)
        {
            Id = id;
            Login = login;
            Hash = hash;
            ApiKey = apiKey;
        }
        public IEnumerable<string> ApiKey { get; }
        public string Login { get; }
        public string Hash { get; set; }
    }
}