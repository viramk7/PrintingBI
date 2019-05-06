using System.ComponentModel.DataAnnotations;

namespace PrintingBI.Authentication.Models.Dtos
{
    public class ForgotPasswordInputDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
