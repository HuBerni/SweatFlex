using System.Security.Cryptography;

namespace SweatFlexUtility
{
    public class HashUtils
    {
        public string GetSalt()
        {
            byte[] bytes = new byte[128 / 8];
            using (var keyGenerator = RandomNumberGenerator.Create())
            {
                keyGenerator.GetBytes(bytes);
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        public string GetPepper()
        {
            return "5098f4b6-33c4-47d5-bd19-2a1d7f6bbf49";
        }
    }
}
