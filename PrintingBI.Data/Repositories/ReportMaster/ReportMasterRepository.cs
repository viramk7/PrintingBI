using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PrintingBI.Data.Entities;
using PrintingBI.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.ReportMaster
{
    public class ReportMasterRepository : IReportMasterRepository
    {
        protected readonly PrintingBIDbContext _context;
        private readonly ILogger<ReportMasterRepository> _logger;

        public ReportMasterRepository(ICustomerDbContext context, ILogger<ReportMasterRepository> logger)
        {
            _logger = logger;
            _context = context.Context;
        }

        public async Task<List<PrinterBIReportMaster>> GetAllReports()
        {
            return await _context.PrinterBIReportMaster.ToListAsync();
        }

        public async Task SyncReports(IEnumerable<PrinterBIReportMaster> reports)
        {
            var oldReports = await _context.PrinterBIReportMaster.ToListAsync();
            _context.PrinterBIReportMaster.RemoveRange(oldReports);
            await _context.SaveChangesAsync();

            await _context.PrinterBIReportMaster.AddRangeAsync(reports);
            await _context.SaveChangesAsync();
        }
    }
}
