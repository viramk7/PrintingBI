using AutoMapper;
using PrintingBI.Data.CustomModel;
using PrintingBI.Data.Entities;
using PrintingBI.Data.Repositories.AssignReportsToall;
using PrintingBI.Data.Repositories.ReportMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintingBI.Services.AssignToAllService
{
    public class AssignToAllService : IAssignToAllService
    {
        private readonly IAssignReportsToAllRepository _assignReportsToAllRepository;
        private readonly IReportMasterRepository _reportMasterRepository;

        public AssignToAllService(IAssignReportsToAllRepository assignReportsToAllRepository, IReportMasterRepository reportMasterRepository)
        {
            _assignReportsToAllRepository = assignReportsToAllRepository;
            _reportMasterRepository = reportMasterRepository;
        }

        public async Task<List<AssignToAllReportDto>> GetAssignToAllReports()
        {
            var assigedReports = await _assignReportsToAllRepository.GetAllAssignToAllReports();
            var allreports = await _reportMasterRepository.GetAllReports();

            var assignToAllReportsList = new List<AssignToAllReportDto>();
            foreach (var report in allreports)
            {
                assignToAllReportsList.Add(new AssignToAllReportDto
                {
                    Id = report.Id,
                    ReportName = report.ReportName,
                    IsAssignedToAllUsers = assigedReports.Any(a => a.ReportId == report.Id)
                });
            }
            return assignToAllReportsList;
        }

        public async Task<string> SaveAssignReportsToAll(List<Guid> reportlist)
        {
            return await _assignReportsToAllRepository.SaveAssignToAllReports(reportlist);
        }
    }
}
