using PrintingBI.Data.Repositories.Provisioning;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrintingBI.Services.Provisioning
{
    public class ProvisioningService : IProvisioningService
    {
        private readonly IProvisioningRepository _provisioningRepository;

        public ProvisioningService(IProvisioningRepository provisioningRepository)
        {
            _provisioningRepository = provisioningRepository;
        }

        public Task<(bool, List<string>)> Provision()
        {
            return _provisioningRepository.Provision();
        }
    }
}
