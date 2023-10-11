using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexUtility
{
    public static class PasswordHash
    {
        public static string Hash (string clearPassword, out string clearSalt)
        {
            clearSalt = GetSalt();

            using (var sha256 = SHA256.Create())
            {
                var hashedPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(clearPassword));
                var hashedSalt = sha256.ComputeHash(Encoding.UTF8.GetBytes(clearSalt));
                var hashedPepper = sha256.ComputeHash(Encoding.UTF8.GetBytes(GetPepper()));
                return Encoding.Default.GetString(hashedSalt) + Encoding.Default.GetString(hashedPassword) + Encoding.Default.GetString(hashedPepper);
            }

        }

        private static string GetSalt()
        {
            byte[] bytes = new byte[128 / 8];
            using (var keyGenerator = RandomNumberGenerator.Create())
            {
                keyGenerator.GetBytes(bytes);
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        private static string GetPepper()
        {
            return "5098f4b6-33c4-47d5-bd19-2a1d7f6bbf49";
        }

        public static bool ValidatePssword(string dbPassword, string clearPassword, string clearSalt)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(clearPassword));
                var hashedSalt = sha256.ComputeHash(Encoding.UTF8.GetBytes(clearSalt));
                var hashedPepper = sha256.ComputeHash(Encoding.UTF8.GetBytes(GetPepper()));
                string password = Encoding.Default.GetString(hashedSalt) + Encoding.Default.GetString(hashedPassword) + Encoding.Default.GetString(hashedPepper);

                return dbPassword == password;
            }            
        }
    }
}
