using System;
using System.Collections.Generic;

namespace Dinkle.Entities.Account.Data
{
    public class AuthorizeResponse
    {
        public AuthorizeResponse(string token, bool isSuccsessful, UserRole? role, IEnumerable<string> messages = null)
        {
            Messages = messages ?? ArraySegment<string>.Empty;
            Token = token;
            IsSuccsessful = isSuccsessful;
            Role = role;
        }

        public UserRole? Role { get; }
        public bool IsSuccsessful { get; }
        public string Token { get; }
        public IEnumerable<string> Messages { get; }
    }
}