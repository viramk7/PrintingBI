using PrintingBI.Data.Infrastructure;
using PrintingBI.Data.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingBI.Data.Repositories.Common
{
    public class CommonRepository : EfRepository<Entities.PrinterBIDepartment>, ICommonRepository
    {
        public CommonRepository(ICustomerDbContext context) : base(context.Context)
        {

        }
    }
}
