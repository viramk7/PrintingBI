using PrintingBI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.ReportMaster
{
    public interface IReportMasterRepository 
    {
        Task<List<PrinterBIReportMaster>> GetAllReports();
        Task SyncReports(IEnumerable<PrinterBIReportMaster> reports);
    }
}
