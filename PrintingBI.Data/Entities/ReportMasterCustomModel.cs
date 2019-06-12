using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingBI.Data.Entities
{
    public class ReportMasterCustomModel
    {
        /// <summary>
        /// Report PK 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Report Name
        /// </summary>
        public string ReportName { get; set; }
    }
}
