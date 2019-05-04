using System.ComponentModel.DataAnnotations;

namespace PrintingBI.API.Models
{
    public class AuthenticateUserInputDto
    {
        [Required]
        [MaxLength(150)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(150)]
        public string Password { get; set; }
    }
}
