using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using PrintingBI.Data.Entities;

namespace PrintingBI.Data
{
    public class PrintingBIDbContext : DbContext
    {
        public PrintingBIDbContext(DbContextOptions options) : base(options)
        {

        }

        // MAKE SURE TO ARRANGE PROPERTIES IN CASCADE DELETE MANNER
        // EX: Put Books first then Authors 
        
        public DbSet<PrinterBIUser> PrinterBIUsers { get; set; }

        public DbSet<PrinterBIDepartment> PrinterBIDepartments { get; set; }

        public DbSet<AssignedReportsToAll> AssignedReportsToAll { get; set; }

        public DbSet<AssignedReportsToUser> AssignedReportsToUser { get; set; }

        public DbSet<BlockedReportsForUser> BlockedReportsForUser { get; set; }

        public DbSet<PrinterBIReportMaster> PrinterBIReportMaster { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(MyLoggerFactory).EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrinterBIUser>().HasData(
                new PrinterBIUser
                {
                    Id = -1,
                    FullName = "admin",
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    Password = "12345",
                    DepartmentId = null,
                    RoleRightsId = null,
                    Token = null,
                    TokenExpiryDate = null,
                    IsSuperAdmin = true,
                    IsPassChange = true
                });
        }

        public static readonly LoggerFactory MyLoggerFactory = new LoggerFactory(
            new[]
            {
                new ConsoleLoggerProvider((category, level) => 
                            category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information, true)
            });
    }
}
