using PrintingBI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.Reports
{
    public interface IReportsRepository
    {
        Task<bool> SyncReports(string connectionString, IEnumerable<PrinterBIReports> newReports);
        Task<IEnumerable<PrinterBIReports>> GetAllReports(string connectionString);
    }
}
