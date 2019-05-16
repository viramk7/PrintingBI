using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.Provisioning
{
    public class ProvisioningRepository : IProvisioningRepository
    {
        private readonly IEnumerable<IProvision> _provisions;

        public ProvisioningRepository(IEnumerable<IProvision> provisions)
        {
            _provisions = provisions;
        }

        public Task<(bool, List<string>)> Provision()
        {
            throw new System.NotImplementedException();
        }
        
        //public async Task<(bool, List<string>)> Provision()
        //{
        //    var createdAll = true;
        //    var errors = new List<string>();

        //    foreach (var table in _tables)
        //    {
        //        if(!await table.Provision())
        //        {
        //            createdAll = false;
        //            errors.Add(table.ErrorMessage);
        //        }
        //    }

        //    return (createdAll, errors);
        //}
    }
}
