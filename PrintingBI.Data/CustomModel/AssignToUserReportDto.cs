using System;

namespace PrintingBI.Data.CustomModel
{
    /// <summary>
    /// Report assigned to user
    /// </summary>
    public class AssignToUserReportDto
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
        /// If ture the report is assigned to user
        /// </summary>
        public bool IsAssigned { get; set; }
    }
}
