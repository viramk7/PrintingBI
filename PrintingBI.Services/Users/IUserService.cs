using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace PrintingBI.Services.Users
{
    public interface IUserService
    {
        Task<(bool, string)> Insert(string connectionString, IFormFile file);
    }
}
