using Microsoft.EntityFrameworkCore;
using PrintingBI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        public async Task Insert(string connectionString, IEnumerable<PrinterBIUser> users)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connection string not provided!");

            var printingBIDbContextFactory = new PrintingBIDbContextFactory();
            var context = printingBIDbContextFactory.Create(connectionString);

            var oldUsers = await context
                        .PrinterBIUsers
                        .Where(w => !w.IsSuperAdmin)
                        .ToListAsync();

            context.PrinterBIUsers.RemoveRange(oldUsers);
            await context.SaveChangesAsync();

            await context.PrinterBIUsers.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }
    }
}
