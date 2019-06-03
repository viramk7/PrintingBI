using PrintingBI.Data.Repositories.Generic;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.UserMaster
{
    public interface IUserMasterRepository : IRepository<Entities.PrinterBIUser>
    {
        Task<string> InsertUser(Entities.PrinterBIUser entity);
    }
}
