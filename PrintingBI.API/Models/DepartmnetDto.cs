using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrintingBI.API.Models
{
    public class DepartmnetDto
    {
        /// <summary>
        /// Primary Key for Departmnet
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Department's Parent Departmnet's GUID 
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Departmnet Name
        /// </summary>
        public string DepartmentName { get; set; }
    }
}
