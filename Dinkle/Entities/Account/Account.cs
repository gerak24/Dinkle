using System;
using Dinkle.Entities.Account.Data;

namespace Dinkle.Entities.Account
{
    public class Account : Entity
    {
        public Account(Guid id,string login, string hash, UserRole role)
        {
            Id = id;
            Login = login;
            Hash = hash;
            Role = role;
        }

        public UserRole Role { get; }
        public string Login { get; }
        public string Hash { get; set; }
    }
}