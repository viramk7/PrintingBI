using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.Provisioning
{
    public class ProvisionDepartmentTable : IProvision
    {
        public string ErrorMessage => "Could not create Department table";

        public async Task<bool> Provision()
        {
            // Create Dept table here
            return true;
        }
    }
}
