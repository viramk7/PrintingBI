using System.ComponentModel.DataAnnotations;

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
