using System.ComponentModel.DataAnnotations;

namespace PrintingBI.API.Models
{
    /// <summary>
    /// Model to reset the password
    /// </summary>
    public class ResetPassDto
    {
        /// <summary>
        /// Host name 
        /// </summary>
        [Required]
        public string HostName { get; set; }

        /// <summary>
        /// Token provided in link sent via email
        /// </summary>
        [Required]
        public string Token { get; set; }

        /// <summary>
        /// Email address
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// New password for the provided email
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }



}
