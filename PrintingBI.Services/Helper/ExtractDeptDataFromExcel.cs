using CsvHelper;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace PrintingBI.Services.Helper
{
    public class ExtractDeptDataFromExcel : IExtractDeptDataFromExcel
    {
        public List<DepartmentFromExcel> GetDepartments(IFormFile file)
        {
            // Extract the departments from excel and fill into the list below

            List<DepartmentFromExcel> departmentlist = new List<DepartmentFromExcel>();

            if (file.FileName.EndsWith(".csv"))
            {
                var csvData = string.Empty;
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    csvData = reader.ReadToEnd();
                }

                int rowNumber = 1;
                foreach (string row in csvData.Split('\n'))
                {
                    if (row == null)
                    {
                        break;
                    }

                    if (rowNumber != 1)
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            departmentlist.Add(new DepartmentFromExcel
                            {
                                DepartmentName = row.Split(',')[0],
                                ParentDepartmentName = row.Split(',')[1].TrimEnd('\r'),
                            });
                            
                        }
                    }
                    rowNumber++;
                }
            }

            return departmentlist;

            //return new List<DepartmentFromExcel>
            //{
            //    new DepartmentFromExcel("abc","xyz"),
            //    new DepartmentFromExcel("abc2","xyz"),
            //    new DepartmentFromExcel("abc3","xyz"),
            //    new DepartmentFromExcel("abc4","abc"),
            //    new DepartmentFromExcel("abc5","abc"),
            //    new DepartmentFromExcel("abc6","abc2"),
            //    new DepartmentFromExcel("abc7","pqr"),
            //    new DepartmentFromExcel("abc8","pqr"),
            //    new DepartmentFromExcel("abc9","abc7"),
            //    new DepartmentFromExcel("abc10",""),
            //    new DepartmentFromExcel("abc11",""),
            //};
        }
    }
}
