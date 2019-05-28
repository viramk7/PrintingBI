using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrintingBI.API.Models
{
    public class UpdateUserDto
    {
        /// <summary>
        /// Full Name 
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }

        /// <summary>
        /// Uesr Name 
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string UserName { get; set; }

        /// <summary>
        /// Password 
        /// </summary>
        [Required]
        [MaxLength(Constants.PasswordMaxLength)]
        public string Password { get; set; }

        /// <summary>
        /// Email address 
        /// </summary>
        [Required]
        [MaxLength(Constants.EmailMaxLength)]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Department GUID 
        /// </summary>
        [Required]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Role Rights GUID - if assigned
        /// </summary>
        public Guid? RoleRightsId { get; set; }
    }
}
