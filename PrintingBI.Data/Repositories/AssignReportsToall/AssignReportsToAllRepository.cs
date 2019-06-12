using Microsoft.EntityFrameworkCore;
using PrintingBI.Data.Entities;
using PrintingBI.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<string> SaveAssignToAllReports(IEnumerable<Guid> reports)
        {
            List<AssignedReportsToAll> reportlist = new List<AssignedReportsToAll>();
            List<Guid> correctReportIdlist = new List<Guid>();
            List<Guid> wrongReportIdlist = new List<Guid>();

            var allreports = await _context.PrinterBIReportMaster.ToListAsync();

            foreach (Guid id in reports)
            {
                if (allreports.Any(x => x.Id == id))
                {
                    correctReportIdlist.Add(id);
                }
                else
                {
                    wrongReportIdlist.Add(id);
                }
            }

            if(wrongReportIdlist.Count > 0)
            {
                return string.Join(',', wrongReportIdlist);
            }
            else
            {
                var oldReports = await _context.AssignedReportsToAll.ToListAsync();
                _context.AssignedReportsToAll.RemoveRange(oldReports);
                await _context.SaveChangesAsync();

                foreach (Guid reportId in reports)
                {
                    reportlist.Add(new AssignedReportsToAll
                    {
                        ReportId = reportId
                    });
                }

                await _context.AssignedReportsToAll.AddRangeAsync(reportlist);
                await _context.SaveChangesAsync();
                return string.Empty;
            }

            
        }
    }
}
