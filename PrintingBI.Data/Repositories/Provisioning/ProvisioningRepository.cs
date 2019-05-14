using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.Provisioning
{
    public class ProvisioningRepository : IProvisioningRepository
    {
        private readonly IEnumerable<IProvisionTable> _tables;

        public ProvisioningRepository(IEnumerable<IProvisionTable> tables)
        {
            _tables = tables;
        }

        public async Task<(bool, List<string>)> Provision()
        {
            var createdAll = true;
            var errors = new List<string>();

            foreach (var table in _tables)
            {
                if(!await table.Provision())
                {
                    createdAll = false;
                    errors.Add(table.ErrorMessage);
                }
            }

            return (createdAll, errors);
        }
    }
}
