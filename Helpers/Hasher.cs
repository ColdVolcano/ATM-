using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace ATMPlus.Helpers
{
    public static class Hasher
    {
        public static string EncryptSHA1(string input)
        {
            string output;
            using (var hasher = SHA1.Create())
            {
                output = string.Concat(hasher.ComputeHash(Encoding.UTF8.GetBytes(input)).Select(c => c.ToString("x2")));
            }
            return output;
        }
    }
}
