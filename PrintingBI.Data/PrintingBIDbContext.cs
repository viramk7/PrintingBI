using Microsoft.EntityFrameworkCore;
using PrintingBI.Data.Entities;

namespace PrintingBI.Data
{
    public class PrintingBIDbContext : DbContext
    {
        public PrintingBIDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<UserMaster> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMaster>().HasData(new UserMaster
            {
                Id = -1,
                Name = "viram",
                Email = "viramk7@gmail.com",
                Password = "123456"
            });
        }
    }
}
