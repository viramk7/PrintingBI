using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrintingBI.API.Models
{
    public class RefreshTokenDto
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
        /// Refresh Token for provided username/email
        /// </summary>
        [Required]
        public string RefreshToken { get; set; }
    }
}
