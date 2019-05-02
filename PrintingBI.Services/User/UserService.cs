using PrintingBI.Data.Entities;
using PrintingBI.Data.Repositories.User;
using PrintingBI.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingBI.Services.User
{
    public class UserService : EntityService<UserMaster>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public bool AuthenticateUser(string email, string password)
        {
            return _userRepository.AuthenticateUser(email,password);
        }

    }
}
