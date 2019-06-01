using PrintingBI.Data.Infrastructure;
using PrintingBI.Data.Repositories.Generic;
using System;
using System.Linq;

namespace PrintingBI.Data.Repositories.UserMaster
{
    public class UserMasterRepository : EfRepository<Entities.PrinterBIUser>, IUserMasterRepository
    {
        public UserMasterRepository(ICustomerDbContext context) : base(context.Context)
        {

        }

        //public void Insert(Entities.PrinterBIUser entity)
        //{
        //    if (entity == null)
        //        throw new ArgumentNullException(nameof(entity));

        //    try
        //    {
        //        var duplicateUser = _context.PrinterBIUsers.Any(f => f.Email.ToLower() == entity.Email);


        //        Entities.Add(entity);
        //        _context.SaveChanges();
        //    }
        //    catch (DbUpdateException exception)
        //    {
        //        //ensure that the detailed error text is saved in the Log
        //        throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
        //    }
        //}
    }
}
