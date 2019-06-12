using Microsoft.EntityFrameworkCore;
using PrintingBI.Data.CustomModel;
using PrintingBI.Data.Entities;
using PrintingBI.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.AssignReportsToUser
{
    public class AssignReportsToUserRepository : IAssignReportsToUserRepository
    {
        protected readonly PrintingBIDbContext _context;

        public AssignReportsToUserRepository(ICustomerDbContext context)
        {
            _context = context.Context;
        }

        public async Task<List<AssignToUserReportDto>> GetAllReportsAssignToUser(int userId)
        {
            var allReports = await _context.PrinterBIReportMaster.ToListAsync();
            var userAssignedReports = await _context.AssignedReportsToUser.Where(x => x.UserId == userId).ToListAsync();
            var assignReportstoAll = await _context.AssignedReportsToAll.ToListAsync();
            var blockedReports = await _context.BlockedReportsForUser.Where(x => x.UserId == userId).ToListAsync();


            var allAssignReportsIds = new List<Guid>();
            foreach (var userReport in userAssignedReports)
            {
                allAssignReportsIds.Add(userReport.ReportId);
            }

            foreach (var allReport in assignReportstoAll)
            {
                allAssignReportsIds.Add(allReport.ReportId);
            }

            foreach (var blocked in blockedReports)
            {
                allAssignReportsIds.Remove(blocked.ReportId);
            }

            var allUserReports = new List<AssignToUserReportDto>();
            foreach (var report in allReports)
            {
                allUserReports.Add(new AssignToUserReportDto
                {
                    Id = report.Id,
                    ReportName = report.ReportName,
                    IsAssigned = allAssignReportsIds.Any(x => x == report.Id)
                });
            }
            return allUserReports;
        }

        public async Task<List<Guid>> SaveAssignReportsToUser(int userId, List<Guid> reports)
        {
            var oldUserReports = await _context.AssignedReportsToUser.Where(x => x.UserId == userId).ToListAsync();
            _context.AssignedReportsToUser.RemoveRange(oldUserReports);
            await _context.SaveChangesAsync();

            var oldBlockedReports = await _context.BlockedReportsForUser.Where(x => x.UserId == userId).ToListAsync();
            _context.BlockedReportsForUser.RemoveRange(oldBlockedReports);
            await _context.SaveChangesAsync();

            var allReports = await _context.PrinterBIReportMaster.ToListAsync();

            var assignedToall = await _context.AssignedReportsToAll.ToListAsync();

            var blockedReports = new List<Guid>();

            foreach (var assignObj in assignedToall)
            {
                if (reports.Any(e => e == assignObj.ReportId))
                {
                    reports.Remove(assignObj.ReportId);
                }
                else
                {
                    blockedReports.Add(assignObj.ReportId);
                }
            }

            List<BlockedReportsForUser> blockedlist = new List<BlockedReportsForUser>();
            foreach (var obj in blockedReports)
            {
                blockedlist.Add(new BlockedReportsForUser
                {
                    ReportId = obj,
                    UserId = userId
                });
            }

            List<Guid> notExistsReports = new List<Guid>();
            List<AssignedReportsToUser> userreportlist = new List<AssignedReportsToUser>();
            foreach (var obj in reports)
            {
                if (allReports.Any(x => x.Id == obj))
                {
                    userreportlist.Add(new AssignedReportsToUser
                    {
                        ReportId = obj,
                        UserId = userId
                    });
                }
                else
                {
                    notExistsReports.Add(obj);
                }
            }

            await _context.BlockedReportsForUser.AddRangeAsync(blockedlist);
            await _context.AssignedReportsToUser.AddRangeAsync(userreportlist);
            await _context.SaveChangesAsync();

            return notExistsReports;
        }
    }
}
