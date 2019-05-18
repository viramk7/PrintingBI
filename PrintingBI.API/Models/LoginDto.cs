using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrintingBI.API.Models
{
    public class LoginDto
    {
        [Required]
        public string HostName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class ForgotPassDto
    {
        [Required]
        public string HostName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }

    public class ResetPassDto
    {
        [Required]
        public string HostName { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class ChangePassDto
    {
        [Required]
        public string HostName { get; set; }

        /// <summary>
        /// Email address of the user 
        /// </summary>
        [Required]
        [EmailAddress]
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

    public class ValidateTenantDto
    {
        [Required]
        public string HostName { get; set; }
    }



}
