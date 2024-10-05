namespace PersonCatalog.Infrastructure.Services;

public class CacheService(IDatabase _database)
    : ICacheService
{
    public async Task<string> GetValueAsync(string key)
    {
        return await _database.StringGetAsync(key);
    }

    public async Task SetValueAsync(string key, string value, TimeSpan? expiry = null)
    {
        _database.StringSetAsync(key, value, expiry);
    }

    public async Task DeleteValueAsync(string key)
    {
        await _database.KeyDeleteAsync(key);
    }

    public async Task CleanAllAsync()
    {
        await _database.ExecuteAsync("FLUSHDB");
    }
}
