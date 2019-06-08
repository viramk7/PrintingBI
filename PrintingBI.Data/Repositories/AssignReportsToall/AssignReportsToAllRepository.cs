using Microsoft.EntityFrameworkCore;
using PrintingBI.Data.Entities;
using PrintingBI.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.AssignReportsToall
{
    public class AssignReportsToAllRepository : IAssignReportsToAllRepository
    {
        protected readonly PrintingBIDbContext _context;

        public AssignReportsToAllRepository(ICustomerDbContext context)
        {
            _context = context.Context;
        }

        public async Task<List<AssignedReportsToAll>> GetAllAssignToAllReports()
        {
            return await _context.AssignedReportsToAll.ToListAsync();
        }

        public async Task SaveAssignToAllReports(IEnumerable<Guid> reports)
        {
            var oldReports = await _context.AssignedReportsToAll.ToListAsync();
            _context.AssignedReportsToAll.RemoveRange(oldReports);
            await _context.SaveChangesAsync();

            List<AssignedReportsToAll> reportlist = new List<AssignedReportsToAll>();

            foreach (Guid reportId in reports)
            {
                reportlist.Add(new AssignedReportsToAll
                {
                    ReportId = reportId
                });
            }

            await _context.AssignedReportsToAll.AddRangeAsync(reportlist);
            await _context.SaveChangesAsync();
        }
    }
}
