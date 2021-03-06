﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using PrintingBI.Data.Entities;

namespace PrintingBI.Services.Helper
{
    public interface IFilterDeptListToEntityHelper
    {
        (bool, IEnumerable<PrinterBIDepartment>) CreateDepartmentHierarchy(IFormFile file);
    }
}