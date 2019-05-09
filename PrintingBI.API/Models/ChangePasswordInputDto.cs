using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrintingBI.API.Models
{
    /// <summary>
    /// Input model for changing current password
    /// </summary>
    public class ChangePasswordInputDto
    {
        /// <summary>
        /// Email address of the user 
        /// </summary>
        [Required]
        [EmailAddress]
        [MaxLength(Constants.EmailMaxLength)]
        public string Email { get; set; }

        /// <summary>
        /// User's old password 
        /// </summary>
        [Required]
        [MaxLength(Constants.PasswordMaxLength)]
        public string OldPassword { get; set; }

        /// <summary>
        /// User's new password
        /// </summary>
        [Required]
        [MaxLength(Constants.PasswordMaxLength)]
        public string NewPassword { get; set; }
    }
}
