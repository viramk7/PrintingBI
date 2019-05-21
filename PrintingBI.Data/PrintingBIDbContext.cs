using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrinterBIUser>().HasData(
                new PrinterBIUser
                {
                    Id = -1,
                    FullName = "admin" ,
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
    }
}
