using Microsoft.AspNetCore.Http;
using PrintingBI.Data.Repositories.Users;
using PrintingBI.Services.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrintingBI.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IFilterUsertListToEntityHelper _filterUseListToEntityHelper;

        public UserService(IUserRepository userRepo,
                                 IFilterUsertListToEntityHelper filterUseListToEntityHelper)
        {
            _userRepo = userRepo;
            _filterUseListToEntityHelper = filterUseListToEntityHelper;
        }

        public async Task Insert(string connectionString, IFormFile file)
        {
            var userslist = _filterUseListToEntityHelper.CreateUserList(file,connectionString);
            await _userRepo.Insert(connectionString, userslist);
        }
    }
}
