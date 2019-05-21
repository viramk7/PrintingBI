using Microsoft.AspNetCore.Http;
using PrintingBI.Data.Entities;
using System;
using System.Collections.Generic;

namespace PrintingBI.Services.Helper
{
    public class FilterDeptListToEntityHelper : IFilterDeptListToEntityHelper
    {
        private readonly IExtractDeptDataFromExcel _departments;

        public FilterDeptListToEntityHelper(IExtractDeptDataFromExcel departments)
        {
            _departments = departments;
        }

        public IEnumerable<PrinterBIDepartment> CreateDepartmentHierarchy(IFormFile file)
        {
            var items = _departments.GetDepartments(file);

            var dataMap = new Dictionary<string, string>();
            items.ForEach(item =>
            {
                dataMap.Add(item.DepartmentName, item.ParentDepartmentName);
            });

            var depts = new Dictionary<string, Guid>();
            foreach (var item in dataMap)
            {
                if (string.IsNullOrEmpty(item.Value))
                    depts.Add(item.Key, Guid.NewGuid());

                if (!string.IsNullOrEmpty(item.Key) && !depts.ContainsKey(item.Key))
                    depts.Add(item.Key, Guid.NewGuid());

                if (!string.IsNullOrEmpty(item.Value) && !depts.ContainsKey(item.Value))
                    depts.Add(item.Value, Guid.NewGuid());
            }

            var departments = new List<PrinterBIDepartment>();
            foreach (var dept in depts)
            {
                var entity = new PrinterBIDepartment
                {
                    Id = dept.Value,
                    ParentId = Guid.Empty,
                    DepartmentName = dept.Key
                };

                if (dataMap.ContainsKey(dept.Key))
                {
                    var val = dataMap[dept.Key];
                    if (!string.IsNullOrEmpty(val))
                        entity.ParentId = depts[val];
                }

                departments.Add(entity);
            }

            return departments;
        }
    }
}
