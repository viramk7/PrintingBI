using Microsoft.AspNetCore.Http;
using PrintingBI.Common;
using PrintingBI.Common.Configurations.FileConfigurations;
using System;
using System.Collections.Generic;

namespace PrintingBI.Services.Helper
{
    public class ExtractUserDataFromExcel : IExtractUserDataFromExcel
    {
        private readonly IUserFileConfig _userFileConfig;

        public ExtractUserDataFromExcel(IUserFileConfig userFileConfig)
        {
            _userFileConfig = userFileConfig;
        }

        public (bool, List<UserFromExcel>) GetUsers(IFormFile file)
        {
            var csvData = CSVHelper.ReadCSVFile(file);
            var rowWiseData = csvData.Split('\n');

            if (!ValidateUserFile(rowWiseData))
                return (false, null);

            int rowNumber = 1;
            var userlist = new List<UserFromExcel>();
            foreach (string row in rowWiseData)
            {
                if (row == null)
                    break;

                if (rowNumber == 1)
                {
                    rowNumber++;
                    continue;
                }


                if (!string.IsNullOrEmpty(row))
                {
                    var rowData = row.Split(',');
                    userlist.Add(new UserFromExcel
                    {
                        UserName = rowData[0],
                        FullName = rowData[1].TrimEnd('\r'),
                        Email = rowData[2].TrimEnd('\r'),
                        DepartmentName = rowData[3].TrimEnd('\r'),
                        RoleRightsName = rowData[4].TrimEnd('\r'),
                    });

                }

                rowNumber++;
            }

            return (true, userlist);
        }

        private bool ValidateUserFile(string[] rowWiseData)
        {
            var firstRow = rowWiseData[0].Split(',');

            if (firstRow.Length < 5)
                return false;

            var firstColumn = firstRow[0];
            var secondColumn = firstRow[1].TrimEnd('\r');
            var thirdColumn = firstRow[2].TrimEnd('\r');
            var forthColumn = firstRow[3].TrimEnd('\r');
            var fifthColumn = firstRow[4].TrimEnd('\r');

            return firstColumn.ToLower() == _userFileConfig.FirstColumnName.ToLower()
                && secondColumn.ToLower() == _userFileConfig.SecondColumnName.ToLower()
                && thirdColumn.ToLower() == _userFileConfig.ThirdColumnName.ToLower()
                && forthColumn.ToLower() == _userFileConfig.ForthColumnName.ToLower()
                && fifthColumn.ToLower() == _userFileConfig.FifthColumnName.ToLower();
        }
    }
}
