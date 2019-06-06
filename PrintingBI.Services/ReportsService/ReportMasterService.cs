using AutoMapper;
using PrintingBI.Data.Entities;
using PrintingBI.Data.Repositories.ReportMaster;
using PrintingBI.Services.PowerBIService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrintingBI.Services.ReportsService
{
    public class ReportMasterService : IReportMasterService
    {
        private readonly IReportMasterRepository _reportMasterRepository;
        private readonly IPowerBIService _powerBIService;
        private readonly IMapper _mapper;

        public ReportMasterService(IReportMasterRepository reportMasterRepository, IPowerBIService powerBIService, IMapper mapper)
        {
            _reportMasterRepository = reportMasterRepository;
            _powerBIService = powerBIService;
            _mapper = mapper;
        }

        public async Task<List<PrinterBIReportMaster>> GetAllReports()
        {
            return await _reportMasterRepository.GetAllReports();
        }

        public async Task SyncReports()
        {
            var powerbireports = _powerBIService.GetReportList();
            var mappedReportlist = _mapper.Map<List<PrinterBIReportMaster>>(powerbireports);
            await _reportMasterRepository.SyncReports(mappedReportlist);
        }

        public async Task<PBReportViewModel> GetSingleReportData(string reportid)
        {
            var powerbireports = _powerBIService.GetPowerBIReport(reportid);
            return powerbireports;
        }
    }
}
