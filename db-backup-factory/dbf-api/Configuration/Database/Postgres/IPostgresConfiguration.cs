namespace dbf_api.Configuration;

public interface IPostgresConfiguration
{
    public string Host { get; }
    public int Port { get; }
    public string? Database { get; }
    public string Username { get; }
    public string Password { get; }
}
