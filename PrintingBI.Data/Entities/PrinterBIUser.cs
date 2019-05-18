using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintingBI.Data.Entities
{
    [Table("PrinterBI_User")]
    public class PrinterBIUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? DepartmentId { get; set; }
        public int? RoleRightsId { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpiryDate { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsPassChange { get; set; }
    }
}
