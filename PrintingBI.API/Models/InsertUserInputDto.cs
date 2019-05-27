using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace PrintingBI.API.Models
{
    public class InsertUserInputDto : CustomerDbCredsInputDto, IValidatableObject
    {
        /// <summary>
        /// File with all the users to be registered
        /// </summary>
        [Required]
        public IFormFile UserFile { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (!Constants.ValidDeptFileUploadExt.Contains(Path.GetExtension(UserFile.FileName)))
                results.Add(new ValidationResult($"Only {Constants.ValidDeptFileUploadExt} files allowed."));

            return results;
        }
    }
}
