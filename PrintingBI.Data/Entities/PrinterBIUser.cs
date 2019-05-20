using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintingBI.Data.Entities
{
    public class PrinterBIUser : Entity<int>
    {
        public string FullName { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(32)]
        public string Password { get; set; }

        [Required]
        [MaxLength(256)]
        public string Email { get; set; }

        public int? DepartmentId { get; set; }
        public int? RoleRightsId { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpiryDate { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsPassChange { get; set; }
    }
}
