using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PrintingBI.API.Models
{
    /// <summary>
    /// Model to provision the tenants
    /// </summary>
    public class CustomerDbCredsInputDto
    {
        /// <summary>
        /// Tenant database name
        /// </summary>
        [Required]
        public string DbName { get; set; }

        /// <summary>
        /// Tenant Db Server name or IP
        /// </summary>
        [Required]
        public string Server { get; set; }

        /// <summary>
        /// Tenant's registered user name 
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
