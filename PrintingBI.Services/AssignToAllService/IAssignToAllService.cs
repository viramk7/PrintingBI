using PrintingBI.Data.CustomModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrintingBI.Services.AssignToAllService
{
    public interface IAssignToAllService
    {
        Task<List<AssignToAllReportDto>> GetAssignToAllReports();
        Task<string> SaveAssignReportsToAll(List<Guid> reportlist);
    }
}
