using PrintingBI.Data.Repositories.ProvisionPowerBITenants;
using System.Threading.Tasks;

namespace PrintingBI.Services.ProvisionPowerBITenants
{
    public class ProvisionPowerBITenantsService : IProvisionPowerBITenantsService
    {
        private readonly IProvisionPowerBITenantsRepository _provisionPowerBITenantsRepository;

        public ProvisionPowerBITenantsService(IProvisionPowerBITenantsRepository provisionPowerBITenantsRepository)
        {
            _provisionPowerBITenantsRepository = provisionPowerBITenantsRepository;
        }

        public Task<(bool, string)> Provision(string connectionString)
        {
            return _provisionPowerBITenantsRepository.Provision(connectionString);
        }
    }
}
