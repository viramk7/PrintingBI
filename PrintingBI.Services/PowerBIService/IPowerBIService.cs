using Microsoft.PowerBI.Api.V2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrintingBI.Services.PowerBIService
{
    public interface IPowerBIService
    {
        List<Report> GetReportList();
        PBReportViewModel GetPowerBIReport(string reportId);
    }
}
