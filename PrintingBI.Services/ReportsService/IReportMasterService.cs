using PrintingBI.Data.Entities;
using PrintingBI.Services.PowerBIService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrintingBI.Services.ReportsService
{
    public interface IReportMasterService
    {
        Task<List<ReportMasterCustomModel>> GetAllReports();
        Task SyncReports();
        Task<PBReportViewModel> GetSingleReportData(string reportid);
    }
}
