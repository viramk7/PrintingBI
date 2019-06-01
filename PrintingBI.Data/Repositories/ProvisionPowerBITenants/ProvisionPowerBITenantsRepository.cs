using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.ProvisionPowerBITenants
{
    public class ProvisionPowerBITenantsRepository : IProvisionPowerBITenantsRepository
    {
        public async Task<(bool, string)> Provision(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connection string not provided!");

            var printingBIDbContextFactory = new PrintingBIDbContextFactory();
            var context = printingBIDbContextFactory.Create(connectionString);

            // Drop table if exists here

            //var tablesToDrop = await File.ReadAllTextAsync("ProvisionScripts/DropTablesIfExists.sql");

            var tablesToDrop = CreateDropTablesScript();
            await context.Database.ExecuteSqlCommandAsync(tablesToDrop);

            var createAllTablesScript = context.Database.GenerateCreateScript();
            await context.Database.ExecuteSqlCommandAsync(createAllTablesScript);

            return (true, "Provisioned successfully.");
        }

        public async Task<(bool, string)> DeProvision(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connection string not provided!");

            var printingBIDbContextFactory = new PrintingBIDbContextFactory();
            var context = printingBIDbContextFactory.Create(connectionString);

            var tablesToDrop = CreateDropTablesScript();
            await context.Database.ExecuteSqlCommandAsync(tablesToDrop);

            return (true, "De-Provisioned successfully.");
        }

        private string CreateDropTablesScript()
        {
            var tablesToDrop = new StringBuilder();
            var tableNames= GetAllEntities();

            tableNames.ForEach(table => 
            {
                tablesToDrop.AppendLine($"DROP TABLE IF EXISTS \"{ table }\";");
            });

            return tablesToDrop.ToString();
        }

        private List<string> GetAllEntities()
        {
            var dbcontextProps =
                typeof(PrintingBIDbContext).BaseType.GetProperties()
                    .Select(p => p.Name).ToList();

            return 
                typeof(PrintingBIDbContext).GetProperties()
                    .Select(p => p.Name).Except(dbcontextProps).ToList();
        }
    }
}
