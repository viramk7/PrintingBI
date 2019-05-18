﻿using System;

namespace PrintingBI.Data.Entities
{
    public class Department : Entity<Guid>
    {
        public Guid? ParentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
