using System.ComponentModel.DataAnnotations;

namespace PrintingBI.API.Models
{
    /// <summary>
    /// Resend confirmation email model
    /// </summary>
    public class ResendConfirmationEmailInputDto
    {
        /// <summary>
        /// Email address of the user
        /// </summary>
        [Required]
        [EmailAddress]
        [MaxLength(Constants.EmailMaxLength)]
        public string Email { get; set; }
    }
}
