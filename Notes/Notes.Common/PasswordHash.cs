using System.Security.Cryptography;
using System.Text;

namespace Notes.Common
{
    public class PasswordHash
    {
        private readonly string _hash;
        const string HEX_DIGITS = "0123456789ABCDEF";
        public PasswordHash(string value)
        {
            _hash = HashString(value);
        }
        public static string HashString(string value)
        {
            HashAlgorithm alg = HashAlgorithm.Create("SHA1");
            byte[] hashValue = alg.ComputeHash(Encoding.UTF8.GetBytes(value));
            StringBuilder builder = new StringBuilder(hashValue.Length * 2);
            foreach (char ch in hashValue)
            {
                builder.Append(HEX_DIGITS[(ch >> 4) & 0x0F]);
                builder.Append(HEX_DIGITS[ch & 0x0F]);
            }
            return builder.ToString();
        }

        public string Hash { get { return _hash; } }
    }
}
