using Microsoft.AspNetCore.Http;
using PrintingBI.Data.Entities;
using PrintingBI.Data.Repositories.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<(bool, string, IEnumerable<PrinterBIUser>)> CreateUserList(IFormFile file, string connectionString)
        {
            var items = _users.GetUsers(file);
            var departmentlist = await _departRepo.GetDepartmentList(connectionString);
            if (!departmentlist.Any())
                return (false, "There are no departments in db. Please add departments.", null);

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
                    var department = departmentlist
                        .FirstOrDefault(m => m.DepartmentName.Equals(userObj.DepartmentName, StringComparison.OrdinalIgnoreCase));

                    if (department != null)
                        entity.DepartmentId = department.Id;
                }

                if (!string.IsNullOrEmpty(userObj.RoleRightsName))
                {
                    var roleRights =
                        departmentlist
                            .FirstOrDefault(m => m.DepartmentName.Equals(userObj.RoleRightsName, StringComparison.OrdinalIgnoreCase));

                    if (roleRights != null)
                        entity.RoleRightsId = roleRights.Id;
                }

                users.Add(entity);
            }


            return (true, string.Empty, users);
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
