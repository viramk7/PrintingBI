using PrintingBI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.AssignReportsToall
{
    public interface IAssignReportsToAllRepository
    {
        Task<List<AssignedReportsToAll>> GetAllAssignToAllReports();
        Task<string> SaveAssignToAllReports(IEnumerable<Guid> reports);
    }
}
