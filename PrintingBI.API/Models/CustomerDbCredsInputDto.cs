using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PrintingBI.API.Models
{
    public class CustomerDbCredsInputDto
    {
        [Required]
        public string DbName { get; set; }

        [Required]
        public string Server { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [ScaffoldColumn(false)]
        public string ConnectionString
        {
            get
            {
                return $"User ID={UserName};password={Password};Server={Server};port=5432;Database={DbName};Integrated Security=true; Pooling=true";
            }
        }

    }
}
