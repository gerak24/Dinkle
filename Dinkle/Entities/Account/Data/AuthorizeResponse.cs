using System;
using System.Collections.Generic;

namespace Dinkle.Entities.Account.Data
{
    public class AuthorizeResponse
    {
        public AuthorizeResponse(string token, bool isSuccsessful, IEnumerable<string> apiKey, IEnumerable<string> messages = null)
        {
            Messages = messages ?? ArraySegment<string>.Empty;
            Token = token;
            IsSuccsessful = isSuccsessful;
            ApiKey = apiKey;
        }
        public bool IsSuccsessful { get; }
        public string Token { get; }
        public IEnumerable<string> Messages { get; }
        public IEnumerable<string> ApiKey { get; }
    }
}