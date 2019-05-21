using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PrintingBI.API.Models
{
    public class InsertUserInputDto : CustomerDbCredsInputDto, IValidatableObject
    {
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
