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
            var builder = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                var enc = Encoding.UTF8;
                var result = hash.ComputeHash(enc.GetBytes(plainText));

                foreach (var b in result)
                {
                    builder.Append(b.ToString("x2"));
                }
            }
            
            return builder.ToString();
        }
    }
}
