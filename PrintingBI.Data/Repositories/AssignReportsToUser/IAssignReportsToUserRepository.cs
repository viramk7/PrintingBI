using PrintingBI.Data.CustomModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.AssignReportsToUser
{
    public interface IAssignReportsToUserRepository
    {
        Task<List<AssignToUserReportDto>> GetAllReportsAssignToUser(int userId);
        Task<List<Guid>> SaveAssignReportsToUser(int userId, List<Guid> reports);
    }
}
