
using System.Security.Cryptography;
using System.Text;

namespace Utils
{
    public static class HashUtils
    {
        private const int keySize = 64;
        private const int iterations = 350000;
        private static byte[] salt = RandomNumberGenerator.GetBytes(keySize);
        private static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public static string HashPassword(string password)
        {
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize
            );

            return Convert.ToBase64String(hash);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize
            );

            return CryptographicOperations.FixedTimeEquals(
                hashToCompare,
                Convert.FromBase64String(hashedPassword)
            );
        }
    }
}