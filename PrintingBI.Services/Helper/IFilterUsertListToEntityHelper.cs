using Microsoft.AspNetCore.Http;
using PrintingBI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingBI.Services.Helper
{
    public interface IFilterUsertListToEntityHelper
    {
        IEnumerable<PrinterBIUser> CreateUserList(IFormFile file, string connectionString);
    }
}
