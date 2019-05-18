using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.ProvisionPowerBITenants
{
    public interface IProvisionPowerBITenantsRepository
    {
        Task<(bool, string)> Provision(string connectionString);
        Task<(bool, string)> DeProvision(string connectionString);
    }
}