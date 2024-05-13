namespace dbf_api.Configuration.Encryption;

public class EncryptionConfiguration
{
    public required byte[] Key { get; set; } = null!;
    public required byte[] IV { get; set; } = null!;

    public static EncryptionConfiguration FromEnvironment()
    {
        var keyStr = Environment.GetEnvironmentVariable("ENCRYPTION_KEY") ?? throw new ArgumentNullException("ENCRYPTION_KEY");
        var ivStr = Environment.GetEnvironmentVariable("ENCRYPTION_IV") ?? throw new ArgumentNullException("ENCRYPTION_IV");
        return new EncryptionConfiguration
        {
            Key = CryptoHelper.CreateSha256Hash(keyStr, 32),
            IV = CryptoHelper.CreateSha256Hash(ivStr, 16)
        };
    }
}
