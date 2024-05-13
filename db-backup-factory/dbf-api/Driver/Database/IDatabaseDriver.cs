namespace dbf_api.Driver.Database;

public interface IDatabaseDriver
{
    public Task<StreamReader> GetBackupStream();
    
}
