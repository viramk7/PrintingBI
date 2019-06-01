using System;

namespace PrintingBI.API.Models
{
    public class DepartmnetDto
    {
        /// <summary>
        /// Primary Key for Departmnet
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Departmnet Name
        /// </summary>
        public string DepartmentName { get; set; }
    }
}
