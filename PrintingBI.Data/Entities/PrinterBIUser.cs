﻿using System;
using System.ComponentModel.DataAnnotations;

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

        public Guid? DepartmentId { get; set; }
        public Guid? RoleRightsId { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpiryDate { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryDate { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsPassChange { get; set; }
    }
}
