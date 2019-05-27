using System.ComponentModel.DataAnnotations;

namespace PrintingBI.API.Models
{
    /// <summary>
    /// Model to authenticate the user
    /// </summary>
    public class AuthenticateUserInputDto
    {
        /// <summary>
        /// Email address
        /// </summary>
        [Required]
        [MaxLength(Constants.EmailMaxLength)]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Password for the provided email
        /// </summary>
        [Required]
        [MaxLength(Constants.PasswordMaxLength)]
        public string Password { get; set; }
    }
}
