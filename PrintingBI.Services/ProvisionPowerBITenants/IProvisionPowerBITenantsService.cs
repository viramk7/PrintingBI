using System.Threading.Tasks;

namespace PrintingBI.Services.ProvisionPowerBITenants
{
    public interface IProvisionPowerBITenantsService
    {
        Task<(bool, string)> Provision(string connectionString);
        Task<(bool, string)> DeProvision(string connectionString);
        Task<bool> ValidateDBInfo(string connectionString);
    }
}