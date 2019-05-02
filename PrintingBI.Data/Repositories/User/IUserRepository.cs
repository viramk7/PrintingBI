using PrintingBI.Data.Entities;
using PrintingBI.Data.Repositories.Generic;

namespace PrintingBI.Data.Repositories.User
{
    public interface IUserRepository : IRepository<UserMaster>
    {
        bool AuthenticateUser(string email, string password);
    }
}