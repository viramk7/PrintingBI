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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
