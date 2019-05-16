using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PrintingBI.Data.Entities
{
    public class PrinterBI_User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> RoleRightsId { get; set; }
        public string Token { get; set; }
        public Nullable<DateTime> TokenExpiryDate { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsPassChange { get; set; }
    }
}
