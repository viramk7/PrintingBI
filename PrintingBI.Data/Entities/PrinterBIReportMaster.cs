using System;
using System.ComponentModel.DataAnnotations;

namespace PrintingBI.Data.Entities
{
    public class PrinterBIReportMaster 
    {
        [Key]
        [Required]
        //[MaxLength(250)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string ReportName { get; set; }
    }
}
