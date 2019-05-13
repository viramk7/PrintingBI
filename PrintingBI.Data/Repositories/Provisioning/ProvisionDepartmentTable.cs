using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingBI.Data.Repositories.Provisioning
{
    public class ProvisionDepartmentTable : IProvisionTable
    {
        public string ErrorMessage => "Could not create Department table";

        public bool Create()
        {
            // Create Dept table here
            return true;
        }
    }
}
