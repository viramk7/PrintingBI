using PrintingBI.Data.Infrastructure;
using PrintingBI.Data.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingBI.Data.Repositories.UserMaster
{
    public class UserMasterRepository : EfRepository<Entities.PrinterBIUser>, IUserMasterRepository
    {
        public UserMasterRepository(ICustomerDbContext context) : base(context.Context)
        {

        }
    }
}
