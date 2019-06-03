using Microsoft.EntityFrameworkCore;
using PrintingBI.Data.Infrastructure;
using PrintingBI.Data.Repositories.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.UserMaster
{
    public class UserMasterRepository : EfRepository<Entities.PrinterBIUser>, IUserMasterRepository
    {
        public UserMasterRepository(ICustomerDbContext context) : base(context.Context)
        {

        }
        
        public async Task<string> InsertUser(Entities.PrinterBIUser entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                bool isduplicateEmail = _context.PrinterBIUsers.Any(f => f.Email.ToLower() == entity.Email.ToLower());
                if (isduplicateEmail) return "Email address is already exists.";

                bool isduplicateUser = _context.PrinterBIUsers.Any(f => f.UserName.ToLower() == entity.UserName.ToLower());
                if (isduplicateUser) return "User Name is already exists";

                Entities.Add(entity);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }
    }
}
