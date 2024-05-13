using System.Security.Cryptography;
using System.Text;


namespace dbf_api;

public class CryptoHelper
{
    // General method to create a cryptographic key or IV using SHA-256
    public static byte[] CreateSha256Hash(string input, int size)
    {
        var inputBytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = SHA256.HashData(inputBytes);
        var result = new byte[size];
        Array.Copy(hashBytes, result, Math.Min(size, hashBytes.Length));
        return result;
    }
}
