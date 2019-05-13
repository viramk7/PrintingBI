namespace PrintingBI.API.Configuration
{
    public interface IJwtConfiguration
    {
        string Audience { get; set; }
        int ExpireTime { get; set; }
        string Issuer { get; set; }
        string Key { get; set; }
    }
}