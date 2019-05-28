using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrintingBI.Data.Entities
{
    public class PrinterBIReports : Entity<int>
    {
        [Required]
        [MaxLength(250)]
        public string UniqueId { get; set; }

        [Required]
        [MaxLength(250)]
        public string ReportName { get; set; }
    }
}
