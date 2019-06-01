using System;

namespace PrintingBI.Services.Helper
{
    public class UserFromExcel
    {
        public UserFromExcel()
        {

        }

        public UserFromExcel(string username, string fullname, string email, string depatmentName, string rolerightsName)
        {
            UserName = username;
            FullName = fullname;
            Email = email;
            DepartmentName = depatmentName;
            RoleRightsName = rolerightsName;
        }

        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }
        public string RoleRightsName { get; set; }

        public int DepartmentId { get; set; }
        public Nullable<int> RoleRightsId { get; set; }
    }
}
