using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace PrintingBI.Services.Departments
{
    public interface IDepartmentService
    {
        Task Insert(string connectionString, IFormFile file);
    }
}