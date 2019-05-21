using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PrintingBI.Services.Helper
{
    public class ExtractUserDataFromExcel : IExtractUserDataFromExcel
    {
        public List<UserFromExcel> GetUsers(IFormFile file)
        {
            // Extract the users from excel and fill into the list below

            List<UserFromExcel> userlist = new List<UserFromExcel>();

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
                            userlist.Add(new UserFromExcel
                            {
                                UserName = row.Split(',')[0],
                                FullName = row.Split(',')[1].TrimEnd('\r'),
                                Email = row.Split(',')[2].TrimEnd('\r'),
                                DepartmentName = row.Split(',')[3].TrimEnd('\r'),
                                RoleRightsName = row.Split(',')[4].TrimEnd('\r'),
                            });

                        }
                    }
                    rowNumber++;
                }
            }

            return userlist;
        }
       
    }
}
