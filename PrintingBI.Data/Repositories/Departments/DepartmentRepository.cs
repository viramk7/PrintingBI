using PrintingBI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.Departments
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public async Task Insert(string connectionString, IEnumerable<Department> departments)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connection string not provided!");

            var printingBIDbContextFactory = new PrintingBIDbContextFactory();
            var context = printingBIDbContextFactory.Create(connectionString);

            await context.Departments.AddRangeAsync(departments);
            await context.SaveChangesAsync();
        }
    }
}
