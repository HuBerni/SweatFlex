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
            var utils = new HashUtils();

            clearSalt = utils.GetSalt();

            using (var sha256 = SHA256.Create())
            {
                var hashedPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(clearPassword));
                var hashedSalt = sha256.ComputeHash(Encoding.UTF8.GetBytes(clearSalt));
                var hashedPepper = sha256.ComputeHash(Encoding.UTF8.GetBytes(utils.GetPepper()));
                return Encoding.Default.GetString(hashedSalt) + Encoding.Default.GetString(hashedPassword) + Encoding.Default.GetString(hashedPepper);
            }

        }

        

        public static bool ValidatePassword(string dbPassword, string clearPassword, string clearSalt)
        {
            var utils = new HashUtils();

            using (var sha256 = SHA256.Create())
            {
                var hashedPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(clearPassword));
                var hashedSalt = sha256.ComputeHash(Encoding.UTF8.GetBytes(clearSalt));
                var hashedPepper = sha256.ComputeHash(Encoding.UTF8.GetBytes(utils.GetPepper()));
                string password = Encoding.Default.GetString(hashedSalt) + Encoding.Default.GetString(hashedPassword) + Encoding.Default.GetString(hashedPepper);

                return dbPassword == password;
            }            
        }

        public static string HashNew(string clearPassword, out string clearSalt, HashUtils hashUtils)
        {
            clearSalt = hashUtils.GetSalt();

            using (var sha256 = SHA256.Create())
            {
                var hashedPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(clearPassword));
                var hashedSalt = sha256.ComputeHash(Encoding.UTF8.GetBytes(clearSalt));
                var hashedPepper = sha256.ComputeHash(Encoding.UTF8.GetBytes(hashUtils.GetPepper()));
                return Encoding.Default.GetString(hashedSalt) + Encoding.Default.GetString(hashedPassword) + Encoding.Default.GetString(hashedPepper);
            }

        }
    }
}
