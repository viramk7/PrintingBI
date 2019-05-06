using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace PrintingBI.Authentication
{
    public class PrintingBIAuthContext : IdentityDbContext<UserMaster>
    {
        public PrintingBIAuthContext(DbContextOptions<PrintingBIAuthContext> options) : base(options)
        {

        }
    }
}
