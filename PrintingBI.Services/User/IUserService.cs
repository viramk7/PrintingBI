using PrintingBI.Data.Entities;
using PrintingBI.Services.Entities;

namespace PrintingBI.Services.User
{
    public interface IUserService : IEntityService<UserMaster>
    {
        bool AuthenticateUser(string email, string password);
    }
}