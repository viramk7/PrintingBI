using System.ComponentModel.DataAnnotations;

namespace PrintingBI.API.Models
{
    /// <summary>
    /// Model to create new author
    /// </summary>
    public class AuthorCreateDto
    {
        /// <summary>
        /// First name of the author
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the auther
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
    }
}
