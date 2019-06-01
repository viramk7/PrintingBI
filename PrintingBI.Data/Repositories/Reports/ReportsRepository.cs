using Microsoft.EntityFrameworkCore;
using PrintingBI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.Reports
{
    public class ReportsRepository : IReportsRepository
    {
        public async Task<bool> SyncReports(string connectionString, IEnumerable<PrinterBIReports> newReports)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connection string not provided!");

            var printingBIDbContextFactory = new PrintingBIDbContextFactory();
            var context = printingBIDbContextFactory.Create(connectionString);

            var oldReports = await context
                        .PrinterBIReports
                        .ToListAsync();

            context.PrinterBIReports.RemoveRange(oldReports);
            await context.SaveChangesAsync();

            await context.PrinterBIReports.AddRangeAsync(newReports);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<PrinterBIReports>> GetAllReports(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connection string not provided!");

            var printingBIDbContextFactory = new PrintingBIDbContextFactory();
            var context = printingBIDbContextFactory.Create(connectionString);

            return await context.PrinterBIReports.ToListAsync();
        }
    }
}
