using System.ComponentModel.DataAnnotations;

namespace PrintingBI.API.Models
{
    public class AuthenticateUserInputDto
    {
        [Required]
        [MaxLength(Constants.EmailMaxLength)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(Constants.PasswordMaxLength)]
        public string Password { get; set; }
    }
}
