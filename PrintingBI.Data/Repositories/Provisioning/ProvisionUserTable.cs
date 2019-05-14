using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PrintingBI.Authentication.Configuration;
using PrintingBI.Data.Infrastructure;
using System;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.Provisioning
{
    public class ProvisionUserTable : IProvisionTable
    {
        private readonly ICustomerDbContext _customerDbContext;

        public ProvisionUserTable(ICustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }

        public string ErrorMessage => "Could not create User table";

        public async Task<bool> Provision()
        {
            // Read it from file
            var query = System.IO.File.ReadAllText("ProvisionScripts/UserTable.sql");

            // Create the User Table here

            var result = await _customerDbContext.Context
                                .Database.ExecuteSqlCommandAsync(query);

            return result > 0;

        }
    }
}
