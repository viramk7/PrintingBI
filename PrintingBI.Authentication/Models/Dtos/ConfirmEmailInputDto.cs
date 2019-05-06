using System.ComponentModel.DataAnnotations;

namespace PrintingBI.Authentication.Models.Dtos
{
    public class ConfirmEmailInputDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
