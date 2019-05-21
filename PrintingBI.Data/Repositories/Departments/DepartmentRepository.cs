using Microsoft.EntityFrameworkCore;
using PrintingBI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.Departments
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public async Task Insert(string connectionString, IEnumerable<PrinterBIDepartment> departments)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connection string not provided!");

            var printingBIDbContextFactory = new PrintingBIDbContextFactory();
            var context = printingBIDbContextFactory.Create(connectionString);

            var oldDepts = await context.PrinterBIDepartments.ToListAsync();
            context.PrinterBIDepartments.RemoveRange(oldDepts);
            await context.SaveChangesAsync();

            await context.PrinterBIDepartments.AddRangeAsync(departments);
            await context.SaveChangesAsync();
        }

        public async Task<List<PrinterBIDepartment>> GetDepartmentList(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connection string not provided!");

            var printingBIDbContextFactory = new PrintingBIDbContextFactory();
            var context = printingBIDbContextFactory.Create(connectionString);

            return await context.PrinterBIDepartments.ToListAsync();
        }
    }
}
