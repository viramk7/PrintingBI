using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace PrintingBI.Services.Helper
{
    public interface IExtractUserDataFromExcel
    {
        (bool, List<UserFromExcel>) GetUsers(IFormFile file);
    }
}
