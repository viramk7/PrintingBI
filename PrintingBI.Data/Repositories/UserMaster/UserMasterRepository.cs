using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PrintingBI.Data.Infrastructure;
using PrintingBI.Data.Repositories.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.UserMaster
{
    public class UserMasterRepository : EfRepository<Entities.PrinterBIUser>, IUserMasterRepository
    {
        private readonly ILogger<UserMasterRepository> _logger;

        public UserMasterRepository(ICustomerDbContext context, ILogger<UserMasterRepository> logger)
            : base(context.Context)
        {
            _logger = logger;
        }

        public async Task<string> InsertUser(Entities.PrinterBIUser entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                bool isdepartmentExists = _context.PrinterBIDepartments.Any(f => f.Id == entity.DepartmentId);
                if (!isdepartmentExists) return "Department does not exists.";

                if (entity.RoleRightsId != Guid.Empty && entity.RoleRightsId != null)
                {
                    bool isroleRightsExists = _context.PrinterBIDepartments.Any(f => f.Id == entity.RoleRightsId);
                    if (!isroleRightsExists) return "Role-Right does not exists.";
                }
                var user = _context.PrinterBIUsers
                    .FirstOrDefault(f => f.UserName.ToLower() == entity.UserName.ToLower()
                                      || f.Email.ToLower() == entity.Email.ToLower());

                if (user != null)
                    return "User with username/email already registered with us. Please try login with it.";

                Entities.Add(entity);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (DbUpdateException exception)
            {
                _logger.LogError(exception.StackTrace);
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        public async Task<string> UpdateUser(Entities.PrinterBIUser entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                bool isdepartmentExists = _context.PrinterBIDepartments.Any(f => f.Id == entity.DepartmentId);
                if (!isdepartmentExists) return "Department does not exists.";

                if (entity.RoleRightsId != Guid.Empty && entity.RoleRightsId != null)
                {
                    bool isroleRightsExists = _context.PrinterBIDepartments.Any(f => f.Id == entity.RoleRightsId);
                    if (!isroleRightsExists) return "Role-Right does not exists.";
                }

                var user = _context.PrinterBIUsers
                    .FirstOrDefault(f => (f.UserName.ToLower() == entity.UserName.ToLower()
                                      || f.Email.ToLower() == entity.Email.ToLower()) && f.Id != entity.Id);

                if (user != null)
                    return "User with username/email already registered with us.";

                Entities.Update(entity);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (DbUpdateException exception)
            {
                _logger.LogError(exception.StackTrace);
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }
    }
}
