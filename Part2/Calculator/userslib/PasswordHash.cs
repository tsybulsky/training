
using System.Security.Cryptography;
using System.Text;

namespace userslib
{
    public static class PasswordHash
    {
        public static string GetHash(string password)
        {
            MD5 hash = MD5.Create();
            byte[] hashBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder(hashBytes.Length * 2);
            for (int i = 0; i < hashBytes.Length; i++)
                builder.Append(hashBytes[i].ToString("x2"));
            return builder.ToString();
        }
    }
}
