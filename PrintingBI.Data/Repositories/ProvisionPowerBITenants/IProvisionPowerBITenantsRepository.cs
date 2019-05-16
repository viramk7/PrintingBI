namespace PrintingBI.Data.Repositories.ProvisionPowerBITenants
{
    public interface IProvisionPowerBITenantsRepository
    {
        System.Threading.Tasks.Task<(bool, string)> Provision(string connectionString);
    }
}