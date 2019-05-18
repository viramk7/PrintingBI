using PrintingBI.Data.CustomModel;
using PrintingBI.Services.HttpClientHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingBI.Services.AdminTenantService
{
    public class AdminTenantService : IAdminTenantService
    {
        private static string validateHostUrl = "api/Settings/GetTenantSetting?hostName=";
        private readonly IHttpClientHelper<CustomerInitialInfoModel> _httpClientHelper;

        public AdminTenantService(IHttpClientHelper<CustomerInitialInfoModel> httpClientHelper)
        {
            _httpClientHelper = httpClientHelper;
        }

        public CustomerInitialInfoModel GetCustomerInialInfo(string hostName)
        {
            string validateUrl = validateHostUrl + hostName;
            CustomerInitialInfoModel intialInfo = _httpClientHelper.Get(validateUrl);
            return intialInfo;
        }

    }
}
