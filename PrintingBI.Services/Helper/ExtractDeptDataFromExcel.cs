using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace PrintingBI.Services.Helper
{
    public class ExtractDeptDataFromExcel : IExtractDeptDataFromExcel
    {
        public List<DepartmentFromExcel> GetDepartments(IFormFile file)
        {
            // Extract the departments from excel and fill into the list below

            return new List<DepartmentFromExcel>
            {
                new DepartmentFromExcel("abc","xyz"),
                new DepartmentFromExcel("abc2","xyz"),
                new DepartmentFromExcel("abc3","xyz"),
                new DepartmentFromExcel("abc4","abc"),
                new DepartmentFromExcel("abc5","abc"),
                new DepartmentFromExcel("abc6","abc2"),
                new DepartmentFromExcel("abc7","pqr"),
                new DepartmentFromExcel("abc8","pqr"),
                new DepartmentFromExcel("abc9","abc7"),
                // new Department("abc10",""),
                // new Department("abc11",""),
            };
        }
    }
}
