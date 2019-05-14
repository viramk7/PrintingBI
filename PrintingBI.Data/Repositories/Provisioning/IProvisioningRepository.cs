using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.Provisioning
{
    public interface IProvisioningRepository
    {
        Task<(bool, List<string>)> Provision();
    }
}