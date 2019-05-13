using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrintingBI.Services.Provisioning
{
    public interface IProvisioningService
    {
        Task<(bool, List<string>)> Create();
    }
}