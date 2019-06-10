using PrintingBI.Data.CustomModel;
using PrintingBI.Data.Repositories.AssignReportsToUser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrintingBI.Services.AssignReportsToUser
{
    public class AssignReportsToUserService : IAssignReportsToUserService
    {
        private readonly IAssignReportsToUserRepository _assignReportsToUSerRepository;

        public AssignReportsToUserService(IAssignReportsToUserRepository assignReportsToUSerRepository)
        {
            _assignReportsToUSerRepository = assignReportsToUSerRepository;
        }

        public async Task<List<AssignToUserReportDto>> GetAllReportsAssignToUser(int userId)
        {
            return await _assignReportsToUSerRepository.GetAllReportsAssignToUser(userId);
        }

        public async Task<List<Guid>> SaveAssignReportsToUser(int userId, List<Guid> reports)
        {
            return await _assignReportsToUSerRepository.SaveAssignReportsToUser(userId, reports);
        }
    }
}
