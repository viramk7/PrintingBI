using Microsoft.AspNetCore.Http;
using PrintingBI.Data.Entities;
using PrintingBI.Data.Repositories.Departments;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingBI.Services.Helper
{
    public class FilterUsertListToEntityHelper : IFilterUsertListToEntityHelper
    {
        private readonly IExtractUserDataFromExcel _users;
        private readonly IDepartmentRepository _departRepo;

        public FilterUsertListToEntityHelper(IExtractUserDataFromExcel users, IDepartmentRepository deptRepo)
        {
            _users = users;
            _departRepo = deptRepo;
        }

        public IEnumerable<PrinterBIUser> CreateUserList(IFormFile file,string connectionString)
        {
            var items = _users.GetUsers(file);
            var departmentlist = _departRepo.GetDepartmentList(connectionString).Result;


            var users = new List<PrinterBIUser>();

            foreach (var userObj in items)
            {
                var entity = new PrinterBIUser
                {
                    UserName = userObj.UserName,
                    FullName = userObj.FullName,
                    Email = userObj.Email,
                    Password = CreatePassword(6)
                };

                if (!string.IsNullOrEmpty(userObj.DepartmentName))
                {
                    if (departmentlist.Exists(m => m.DepartmentName == userObj.DepartmentName))
                    {
                        entity.DepartmentId = departmentlist.Find(m => m.DepartmentName == userObj.DepartmentName).Id; ;
                    }
                }

                if (!string.IsNullOrEmpty(userObj.RoleRightsName))
                {
                    if (departmentlist.Exists(m => m.DepartmentName == userObj.RoleRightsName))
                    {
                        entity.RoleRightsId = departmentlist.Find(m => m.DepartmentName == userObj.RoleRightsName).Id;
                    }
                }

                users.Add(entity);
            }

            return users;
        }

        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
