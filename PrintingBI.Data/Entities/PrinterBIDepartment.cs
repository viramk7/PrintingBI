using System;

namespace PrintingBI.Data.Entities
{
    public class PrinterBIDepartment : Entity<Guid>
    {
        public Guid? ParentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
