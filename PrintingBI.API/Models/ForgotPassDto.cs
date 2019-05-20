using System.ComponentModel.DataAnnotations;

namespace PrintingBI.API.Models
{
    /// <summary>
    /// The model to send if user forgets password
    /// </summary>
    public class ForgotPassDto
    {
        /// <summary>
        /// Host name
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string HostName { get; set; }

        /// <summary>
        /// Email address of the user to reset the password
        /// </summary>
        [Required]
        [EmailAddress]
        [MaxLength(Constants.EmailMaxLength)]
        public string EmailAddress { get; set; }
    }
}
