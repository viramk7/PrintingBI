using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrintingBI.API.Models
{
    public class AuthorUpdateDto : IValidatableObject
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (FirstName == LastName)
                results.Add(new ValidationResult("First name and Last name should be different"
                    ,new List<string>() { "Name check" } ));

            return results;
        }
    }
}
