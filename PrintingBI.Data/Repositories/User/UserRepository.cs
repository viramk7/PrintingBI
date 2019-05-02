using PrintingBI.Data.Entities;
using PrintingBI.Data.Repositories.Generic;
using System;
using System.Linq;

namespace PrintingBI.Data.Repositories.User
{
    public class UserRepository : EfRepository<UserMaster>, IUserRepository
    {
        public UserRepository(PrintingBIDbContext context) : base(context)
        {
            
        }

        public bool AuthenticateUser(string email, string password)
        {
            var user = _entities.Where(w => w.Email.Equals(email, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (user == null)
                return false;

            if (user.Password != password)
                return false;

            return true;
        }
    }
}
