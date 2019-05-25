using Microsoft.AspNetCore.Http;
using PrintingBI.Common;
using PrintingBI.Common.Configurations.FileConfigurations;
using System.Collections.Generic;

namespace PrintingBI.Services.Helper
{
    public class ExtractDeptDataFromExcel : IExtractDeptDataFromExcel
    {
        private readonly IDepartmentFileConfig _departmentFileConfig;

        public ExtractDeptDataFromExcel(IDepartmentFileConfig departmentFileConfig)
        {
            _departmentFileConfig = departmentFileConfig;
        }

        public (bool, List<DepartmentFromExcel>) GetDepartments(IFormFile file)
        {
            var csvData = CSVHelper.ReadCSVFile(file);
            var rowWiseData = csvData.Split('\n');

            if (!ValidateFile(rowWiseData))
                return (false, null);

            var rowNumber = 1;
            var departmentlist = new List<DepartmentFromExcel>();
            foreach (string row in rowWiseData)
            {
                if (row == null)
                    break;

                if (rowNumber == 1)
                    continue;

                if (!string.IsNullOrEmpty(row))
                {
                    var rowData = row.Split(',');
                    departmentlist.Add(new DepartmentFromExcel
                    {
                        DepartmentName = rowData[0],
                        ParentDepartmentName = rowData[1]?.TrimEnd('\r'),
                    });
                }

                rowNumber++;
            }

            return (true, departmentlist);
        }

        private bool ValidateFile(string[] rowWiseData)
        {
            var firstRow = rowWiseData[0].Split(',');
            var firstColumn = firstRow[0];
            var SecondColumn = firstRow[1];

            return firstColumn.ToLower() == _departmentFileConfig.FirstColumnName.ToLower() &&
                   SecondColumn.TrimEnd('\r').ToLower() == _departmentFileConfig.SecondColumnName.ToLower();
        }
    }
}
