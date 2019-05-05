using System.ComponentModel.DataAnnotations;

namespace PrintingBI.API.Models
{
    public class AuthorCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
    }
}
