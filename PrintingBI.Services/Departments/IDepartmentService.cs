using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace PrintingBI.Services.Departments
{
    public interface IDepartmentService
    {
        Task<(bool, string)> Insert(string connectionString, IFormFile file);
    }
}