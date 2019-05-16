namespace PrintingBI.Services.ProvisionPowerBITenants
{
    public interface IProvisionPowerBITenantsService
    {
        System.Threading.Tasks.Task<(bool, string)> Provision(string connectionString);
    }
}