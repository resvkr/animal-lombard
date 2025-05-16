using System.Security.Cryptography;
using System.Text;

namespace AnimalLombard.Utils;

public static class HashUtils
{
    private const int KeySize = 64;
    private const int Iterations = 350000;
    private static readonly byte[] Salt = RandomNumberGenerator.GetBytes(KeySize);
    private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512;

    public static string HashPassword(string password)
    {
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            Salt,
            Iterations,
            HashAlgorithm,
            KeySize
        );

        return Convert.ToBase64String(hash);
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            Salt,
            Iterations,
            HashAlgorithm,
            KeySize
        );

        return CryptographicOperations.FixedTimeEquals(
            hashToCompare,
            Convert.FromBase64String(hashedPassword)
        );
    }
}