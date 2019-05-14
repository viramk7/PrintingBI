using Microsoft.EntityFrameworkCore;
using PrintingBI.Data.Infrastructure;

namespace PrintingBI.Data
{
    public class PrintingBIDbContextFactory : DesignTimeDbContextFactoryBase<PrintingBIDbContext>
    {
        protected override PrintingBIDbContext CreateNewInstance(DbContextOptions<PrintingBIDbContext> options)
        {
            return new PrintingBIDbContext(options);
        }
    }
}
