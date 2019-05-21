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
        public async Task Insert(string connectionString, IEnumerable<Department> departments)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connection string not provided!");

            var printingBIDbContextFactory = new PrintingBIDbContextFactory();
            var context = printingBIDbContextFactory.Create(connectionString);

            var oldDepts = await context.Departments.ToListAsync();
            context.Departments.RemoveRange(oldDepts);
            await context.SaveChangesAsync();

            await context.Departments.AddRangeAsync(departments);
            await context.SaveChangesAsync();
        }

        public async Task<List<Department>> GetDepartmentList(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connection string not provided!");

            var printingBIDbContextFactory = new PrintingBIDbContextFactory();
            var context = printingBIDbContextFactory.Create(connectionString);

            List<Department>  departementList = await context.Departments.ToListAsync();
            return departementList;
        }
    }
}
