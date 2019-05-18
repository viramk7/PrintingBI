using PrintingBI.Data.CustomModel;
using System.Threading.Tasks;

namespace PrintingBI.Services.AdminTenantService
{
    public interface IAdminTenantService
    {
        Task<CustomerInitialInfoModel> GetCustomerInialInfo(string hostName);
    }
}
