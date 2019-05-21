using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrintingBI.Services.Users
{
    public interface IUserService
    {
        Task Insert(string connectionString, IFormFile file);
    }
}
