using System;
using System.Text;
using System.Security.Cryptography;

namespace testsss
{
    internal class Program
    {
        private const string SALT = "JOSH17ROGjosh17rog!@#123ABCD";

        static void Main(string[] args)
        {

            for (int i = 0; i < 5; i++) 
            {
                Console.WriteLine("Password Hashing Test Console");
                Console.WriteLine("============================");

                // Test hashing a password
                Console.Write("Enter a password to hash: ");
                string password = Console.ReadLine();

                string hashedPassword = GetSHA256Hash(password);

                Console.WriteLine("\nResults:");
                Console.WriteLine($"Original password: {password}");
                Console.WriteLine($"Hashed password: {hashedPassword}");
                Console.WriteLine($"Hash length: {hashedPassword.Length} characters");

            }

            // Test password verification
            //Console.WriteLine("\nPassword Verification Test");
            //Console.Write("Enter the same password again to verify: ");
            //string verifyPassword = Console.ReadLine();

            //string hashedVerifyPassword = GetSHA256Hash(verifyPassword);

            //Console.WriteLine($"\nHashed verification password: {hashedVerifyPassword}");
            //Console.WriteLine($"Do the hashes match? {hashedPassword == hashedVerifyPassword}");

            
        }

        public static string GetSHA256Hash(string plainText)
        {
            plainText += SALT;
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