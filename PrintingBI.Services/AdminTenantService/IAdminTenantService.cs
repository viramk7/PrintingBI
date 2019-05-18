using PrintingBI.Data.CustomModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingBI.Services.AdminTenantService
{
    public interface IAdminTenantService
    {
        CustomerInitialInfoModel GetCustomerInialInfo(string hostName);
    }
}
