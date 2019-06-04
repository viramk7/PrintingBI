using PrintingBI.Data.Entities;
using PrintingBI.Services.Entities;
using System.Threading.Tasks;

namespace PrintingBI.Services.UserMaster
{
    public interface IUserMasterService : IEntityService<Data.Entities.PrinterBIUser>
    {
        Task<string> InsertUser<CreateUserDto>(CreateUserDto dto);
        Task<string> UpdateUser<UpdateUserDto>(int id, UpdateUserDto dto);
    }
}
