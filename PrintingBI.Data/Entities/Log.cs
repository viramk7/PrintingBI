using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrintingBI.Data.Entities
{
    public class Log : Entity<int>
    {
        [Required]
        [MaxLength(50)]
        public string MachineName { get; set; }

        [Required]
        public DateTime Logged { get; set; }

        [Required]
        [MaxLength(50)]
        public string Level { get; set; }

        [Required]
        public string Message { get; set; }

        [MaxLength(250)]
        public string Logger { get; set; }

        public string Callsite { get; set; }
        public string Exception { get; set; }
        
    }
}
