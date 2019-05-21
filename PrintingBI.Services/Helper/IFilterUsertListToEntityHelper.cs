using Microsoft.AspNetCore.Http;
using PrintingBI.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrintingBI.Services.Helper
{
    public interface IFilterUsertListToEntityHelper
    {
        Task<(bool, string, IEnumerable<PrinterBIUser>)> CreateUserList(IFormFile file, string connectionString);
    }
}
