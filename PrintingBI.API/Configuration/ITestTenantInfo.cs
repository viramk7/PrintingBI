namespace PrintingBI.API.Configuration
{
    public interface ITestTenantInfo
    {
        string TenantDBName { get; set; }
        string TenantDBPassword { get; set; }
        string TenantDBServer { get; set; }
        string TenantDBUser { get; set; }
        bool UseTestTenantInfo { get; set; }
    }
}