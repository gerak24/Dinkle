using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Dinkle.Application.Accounts.Utils
{
    public class AuthOptions
    {
        public const string Issuer = "MyAuthServer";
        public const string Audience = "MyAuthClient";
        const string Key = "mysupersecret_secretkey!123";
        public const int Lifetime = 60;
        public static SymmetricSecurityKey GetSymmetricSecurityKey() => new(Encoding.ASCII.GetBytes(Key));
    }
}