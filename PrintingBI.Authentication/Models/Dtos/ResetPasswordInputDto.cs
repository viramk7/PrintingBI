using System.ComponentModel.DataAnnotations;

namespace PrintingBI.Authentication.Models.Dtos
{
    public class ResetPasswordInputDto
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
