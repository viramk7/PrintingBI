using Microsoft.EntityFrameworkCore;
using PrintingBI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.Reports
{
    public class ReportsRepository : IReportsRepository
    {
        public async Task<bool> SyncReports(string connectionString, IEnumerable<PrinterBIReportMaster> newReports)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connection string not provided!");

            var printingBIDbContextFactory = new PrintingBIDbContextFactory();
            var context = printingBIDbContextFactory.Create(connectionString);

            var oldReports = await context
                        .PrinterBIReportMaster
                        .ToListAsync();

            context.PrinterBIReportMaster.RemoveRange(oldReports);
            await context.SaveChangesAsync();

            await context.PrinterBIReportMaster.AddRangeAsync(newReports);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<PrinterBIReportMaster>> GetAllReports(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connection string not provided!");

            var printingBIDbContextFactory = new PrintingBIDbContextFactory();
            var context = printingBIDbContextFactory.Create(connectionString);

            return await context.PrinterBIReportMaster.ToListAsync();
        }
    }
}
