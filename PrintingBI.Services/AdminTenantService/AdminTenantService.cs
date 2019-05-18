using PrintingBI.Data.CustomModel;
using PrintingBI.Services.HttpClientHelpers;
using System.Threading.Tasks;

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

        public async Task<CustomerInitialInfoModel> GetCustomerInialInfo(string hostName)
        {
            var validateUrl = validateHostUrl + hostName;
            var intialInfo = await _httpClientHelper.Get(validateUrl);
            return intialInfo;
        }

    }
}
