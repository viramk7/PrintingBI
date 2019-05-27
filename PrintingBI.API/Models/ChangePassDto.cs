using System.ComponentModel.DataAnnotations;

namespace PrintingBI.API.Models
{
    /// <summary>
    /// Model to change the password
    /// </summary>
    public class ChangePassDto
    {
        /// <summary>
        /// Host name
        /// </summary>
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



}
