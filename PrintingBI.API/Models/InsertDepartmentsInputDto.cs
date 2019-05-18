using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace PrintingBI.API.Models
{
    public class InsertDepartmentsInputDto : CustomerDbCredsInputDto, IValidatableObject
    {
        [Required]
        public IFormFile DepartmentFile { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (!Constants.ValidDeptFileUploadExt.Contains(Path.GetExtension(DepartmentFile.FileName)))
                results.Add(new ValidationResult($"Only {Constants.ValidDeptFileUploadExt} files allowed."));

            return results;
        }
    }
}
