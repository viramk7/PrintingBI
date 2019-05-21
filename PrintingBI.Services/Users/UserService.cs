using Microsoft.AspNetCore.Http;
using PrintingBI.Data.Repositories.Users;
using PrintingBI.Services.Helper;
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

        public async Task<(bool,string)> Insert(string connectionString, IFormFile file)
        {
            var (isSuccess,message, userslist) = await _filterUseListToEntityHelper.CreateUserList(file,connectionString);
            if (!isSuccess)
                return (isSuccess, message);

            await _userRepo.Insert(connectionString, userslist);
            return (isSuccess, string.Empty);
        }
    }
}
