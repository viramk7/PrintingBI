using PrintingBI.Common.Configurations;

namespace PrintingBI.Data.Infrastructure
{
    public class CustomerDbContext : ICustomerDbContext
    {
        public PrintingBIDbContext Context { get; private set; }

        public CustomerDbContext(ICustomerDbInfo customerDbInfo)
        {
            var printingBIDbContextFactory = new PrintingBIDbContextFactory();
            Context = printingBIDbContextFactory.Create(customerDbInfo.GetCustomerDbConnectionString());
        }
    }
}
