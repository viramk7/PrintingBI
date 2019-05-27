using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PrintingBI.API.Models
{
    /// <summary>
    /// Model for tenant user credentials
    /// </summary>
    public class CustomerDbCredsInputDto
    {
        /// <summary>
        /// Database name
        /// </summary>
        [Required]
        public string DbName { get; set; }

        /// <summary>
        /// Server name or IP
        /// </summary>
        [Required]
        public string Server { get; set; }

        /// <summary>
        /// registered user name with tenant
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Password for the provided user name
        /// </summary>
        [Required]
        public string Password { get; set; }
        
        public string GetConnectionString()
        {
            return $"User ID={UserName};password={Password};Server={Server};port=5432;Database={DbName};Integrated Security=true; Pooling=true";
        }

    }
}
