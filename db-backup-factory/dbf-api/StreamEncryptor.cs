using System.Security.Cryptography;

namespace dbf_api;

public static class StreamEncryptor
{
    public static Stream EncryptStream(Stream inputStream, byte[] key, byte[] iv)
    {
        // Ensure key and IV are the correct size for AES (key: 256 bits, IV: 128 bits)
        if (key.Length != 32 || iv.Length != 16)
            throw new ArgumentException("Key and IV must be valid sizes for AES.");

        Aes aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;
        
        // Create an encryptor from the AES instance
        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        // Create a CryptoStream that wraps the input stream and encrypts it
        CryptoStream cryptoStream = new(inputStream, encryptor, CryptoStreamMode.Read);
        
        // Create a MemoryStream to output the encrypted data
        MemoryStream outputStream = new();

        // Copy data from the CryptoStream to the MemoryStream to perform the encryption
        cryptoStream.CopyTo(outputStream);

        // Reset the MemoryStream position to the beginning for reading
        outputStream.Seek(0, SeekOrigin.Begin);

        // Cleanup
        cryptoStream.Close();
        aes.Dispose();

        return outputStream;
    }

}
