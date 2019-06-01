using PrintingBI.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.Users
{
    public interface IUserRepository
    {
        Task Insert(string connectionString, IEnumerable<PrinterBIUser> users);
    }
}
