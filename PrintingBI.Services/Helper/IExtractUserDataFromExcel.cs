using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingBI.Services.Helper
{
    public interface IExtractUserDataFromExcel
    {
        List<UserFromExcel> GetUsers(IFormFile file);
    }
}
