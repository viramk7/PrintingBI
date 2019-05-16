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

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }
        
        public DbSet<UserMaster> Users { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Log> AppLogs { get; set; }

        public DbSet<ClassRoom> ClassRooms { get; set; }

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
