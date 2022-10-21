using System;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Dinkle.Application.Accounts.Utils
{
    public static class Hasher
    {
        public static string Create(string value, string salt) =>
            Convert.ToBase64String(KeyDerivation.Pbkdf2(password: value, salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA512, iterationCount: 10000, numBytesRequested: 256 / 8));

        public static bool Validate(string value, string salt, string hash)
            => Create(value, salt) == hash;
    }
}