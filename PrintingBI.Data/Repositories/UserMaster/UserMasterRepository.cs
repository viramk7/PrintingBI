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
    }
}
