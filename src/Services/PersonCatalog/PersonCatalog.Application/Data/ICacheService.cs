namespace PersonCatalog.Application.Data;

public interface ICacheService
{
    Task<string> GetValueAsync(string key);
    Task SetValueAsync(string key, string value, TimeSpan? expiry = null);
    Task DeleteValueAsync(string key);
    Task CleanAllAsync();
}

public static class CacheKey
{
    public static string PersonsData { get;} = "persons";
}
