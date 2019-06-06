using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrintingBI.Data.Entities
{
    public class BlockedReportsForUser : Entity<int>
    {
        [Required]
        public Guid ReportId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
