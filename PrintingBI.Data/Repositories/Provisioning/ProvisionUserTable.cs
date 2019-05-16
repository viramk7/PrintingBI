using Microsoft.EntityFrameworkCore;
using PrintingBI.Data.Infrastructure;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.Provisioning
{
    public class ProvisionUserTable : IProvision
    {
        private readonly ICustomerDbContext _customerDbContext;

        public ProvisionUserTable(ICustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }

        public string ErrorMessage => "Could not create User table";

        public async Task<bool> Provision()
        {
            // TODO: Drop tables if exists
            var dropIfExistsTableScript = "";
            await _customerDbContext.Context.Database.ExecuteSqlCommandAsync(dropIfExistsTableScript);

            // Create the User Table here
            var createAllTablesScript = _customerDbContext.Context.Database.GenerateCreateScript();
            var result = await _customerDbContext.Context
                                .Database.ExecuteSqlCommandAsync(createAllTablesScript);

            return result > 0;

        }
    }
}
