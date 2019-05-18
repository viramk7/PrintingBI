using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace PrintingBI.Services.Helper
{
    public interface IExtractDeptDataFromExcel
    {
        List<DepartmentFromExcel> GetDepartments(IFormFile file);
    }
}