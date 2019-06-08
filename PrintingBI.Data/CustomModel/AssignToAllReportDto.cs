using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingBI.Data.CustomModel
{
    public class AssignToAllReportDto
    {
        /// <summary>
        /// Report Guid
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Report Name
        /// </summary>
        public string ReportName { get; set; }

        /// <summary>
        /// Is Checked - if ture means report is assign to all
        /// </summary>
        public bool IsChecked { get; set; }
    }
}
