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

        public async Task<(bool,string)> SaveAssignReportsToAll(List<Guid> reportlist)
        {
            List<Guid> correctReportIdlist = new List<Guid>();
            List<Guid> wrongReportIdlist = new List<Guid>();

            var allreports = await _reportMasterRepository.GetAllReports();

            foreach(Guid id in reportlist)
            {
                if(allreports.Any(x=>x.Id == id))
                {
                    correctReportIdlist.Add(id);
                }
                else
                {
                    wrongReportIdlist.Add(id);
                }
            }

            if(correctReportIdlist.Count > 0)
            {
                await _assignReportsToAllRepository.SaveAssignToAllReports(correctReportIdlist);
            }
            if(wrongReportIdlist.Count > 0)
            {
                return (false, String.Join(',', wrongReportIdlist));
            }
            return (true, "All reports saved suuceefully.");
        }
    }
}
