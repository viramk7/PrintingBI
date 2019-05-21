using System.ComponentModel.DataAnnotations;

namespace PrintingBI.API.Models
{
    /// <summary>
    /// Model to validate the tenant by host name
    /// </summary>
    public class ValidateTenantDto
    {
        /// <summary>
        /// Host name to validate
        /// </summary>
        [Required]
        public string HostName { get; set; }
    }



}
