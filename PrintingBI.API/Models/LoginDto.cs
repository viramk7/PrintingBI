using System.ComponentModel.DataAnnotations;

namespace PrintingBI.API.Models
{
    /// <summary>
    /// The model to authenticate user for given hostname
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Host name
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string HostName { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [MaxLength(32)] 
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
