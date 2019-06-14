namespace PrintingBI.API.Configuration
{
    public class TestTenantInfo : ITestTenantInfo
    {
        public bool UseTestTenantInfo { get; set; }
        public string TenantDBServer { get; set; }
        public string TenantDBName { get; set; }
        public string TenantDBUser { get; set; }
        public string TenantDBPassword { get; set; }
    }
}
