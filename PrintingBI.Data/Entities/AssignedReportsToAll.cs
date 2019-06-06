using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrintingBI.Data.Entities
{
    public class AssignedReportsToAll : Entity<int>
    {
        [Required]
        public Guid ReportId { get; set; }
    }
}
