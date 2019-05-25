using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace PrintingBI.Services.Helper
{
    public interface IExtractDeptDataFromExcel
    {
        (bool, List<DepartmentFromExcel>) GetDepartments(IFormFile file);
    }
}