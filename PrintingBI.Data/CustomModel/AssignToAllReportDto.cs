using System;

namespace PrintingBI.Data.CustomModel
{
    /// <summary>
    /// Report assigned to all the users
    /// </summary>
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
        /// If true the report is assigned to all users
        /// </summary>
        public bool IsAssignedToAllUsers { get; set; }
    }
}
