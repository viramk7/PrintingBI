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
}
