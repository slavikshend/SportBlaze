using System;
using System.Security.Cryptography;
using System.Text;

namespace webapi.BLL.Helpers
{
    public class PasswordHasher
    {
        public (string, string) HashPassword(string password)
        {
            string salt = GenerateSalt();
            string hashedPassword = ComputeHash(password, salt);
            return (hashedPassword, salt);
        }

        public bool VerifyPassword(string password, string hashedPassword, string salt)
        {
            string computedHash = ComputeHash(password, salt);
            return computedHash == hashedPassword;
        }

        private string ComputeHash(string password, string salt)
        {
            string saltedPassword = password + salt;
            byte[] passwordBytes = Encoding.UTF8.GetBytes(saltedPassword);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        private string GenerateSalt()
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            char[] saltChars = new char[16];
            for (int i = 0; i < 16; i++)
            {
                saltChars[i] = chars[random.Next(chars.Length)];
            }
            return new string(saltChars);
        }
    }
}
