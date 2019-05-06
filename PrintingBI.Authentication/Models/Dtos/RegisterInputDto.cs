using System.ComponentModel.DataAnnotations;

namespace PrintingBI.Authentication.Models.Dtos
{
    public class RegisterInputDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
