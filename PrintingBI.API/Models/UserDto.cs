using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrintingBI.API.Models
{
    public class UserDto
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Full Name 
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Uesr Name 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Email address 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Department GUID 
        /// </summary>
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Role Rights GUID - if assigned
        /// </summary>
        public Guid? RoleRightsId { get; set; }

        /// <summary>
        /// Is Super Admin
        /// </summary>
        public bool IsSuperAdmin { get; set; }

        /// <summary>
        /// Is Change pass done for user 
        /// </summary>
        public bool IsPassChange { get; set; }
    }
}
