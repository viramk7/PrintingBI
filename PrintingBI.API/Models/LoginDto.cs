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
        /// Provide the user name or the email of the user
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string UserNameOrEmail { get; set; }

        /// <summary>
        /// Password for provided username/email
        /// </summary>
        [Required]
        [MaxLength(32)] 
        public string Password { get; set; }
    }
}
