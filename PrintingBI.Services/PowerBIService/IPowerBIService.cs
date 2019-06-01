using Microsoft.PowerBI.Api.V2.Models;
using System.Collections.Generic;

namespace PrintingBI.Services.PowerBIService
{
    public interface IPowerBIService
    {
        List<Report> GetReportList();
        PBReportViewModel GetPowerBIReport(string reportId);
    }
}
