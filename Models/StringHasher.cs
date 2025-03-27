using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class StringHasher
    {
        public static string GetSHA256Hash(string plainText)
        {
            StringBuilder builder = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(plainText));

                foreach (byte b in result)
                {
                    builder.Append(b.ToString("x2"));
                }
            }
            
            return builder.ToString();
        }
    }
}
