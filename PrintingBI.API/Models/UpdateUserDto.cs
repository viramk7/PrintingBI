using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrintingBI.API.Models
{
    public class UpdateUserDto
    {
        /// <summary>
        /// Full Name 
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }

        /// <summary>
        /// Uesr Name 
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string UserName { get; set; }

        /// <summary>
        /// Password 
        /// </summary>
        [Required]
        [MaxLength(Constants.PasswordMaxLength)]
        public string Password { get; set; }

        /// <summary>
        /// Email address 
        /// </summary>
        [Required]
        [MaxLength(Constants.EmailMaxLength)]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Department GUID 
        /// </summary>
        [Required]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Role Rights GUID - if assigned
        /// </summary>
        public Guid? RoleRightsId { get; set; }

        /// <summary>
        /// Is User - Super admin ?
        /// </summary>
        [Required]
        public bool IsSuperAdmin { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (DepartmentId == Guid.Empty)
                errors.Add(new ValidationResult("Please provide Department Id",
                    new List<string> { nameof(DepartmentId) }));

            return errors;

        }
    }
}
