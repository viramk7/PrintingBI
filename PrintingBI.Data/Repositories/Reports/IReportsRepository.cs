using PrintingBI.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.Reports
{
    public interface IReportsRepository
    {
        Task<bool> SyncReports(string connectionString, IEnumerable<PrinterBIReportMaster> newReports);
        Task<IEnumerable<PrinterBIReportMaster>> GetAllReports(string connectionString);
    }
}
