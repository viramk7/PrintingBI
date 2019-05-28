using PrintingBI.Data.CustomModel;
using PrintingBI.Data.Repositories.Login;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrintingBI.Services.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<AuthenticateUserResultDto> AuthenticateUser(string connectionString, string userNameOrEmail, string password)
        {
            return await _loginRepository.AuthenticateUser(connectionString, userNameOrEmail, password);
        }

        public async Task<bool> AuthenticateUserByEmail(string connectionString, string Email)
        {
            return await _loginRepository.AuthenticateUserByEmail(connectionString, Email);
        }

        public async Task<string> GeneratePasswordResetToken(string connectionString, string email)
        {
            return await _loginRepository.GeneratePasswordResetToken(connectionString, email);
        }

        public async Task<string> ResetUserPassByToken(string connectionString, string email , string token, string password)
        {
            return await _loginRepository.ResetUserPassByToken(connectionString, email, token, password);
        }

        public async Task<bool> ChangeUserPassword(string connectionString, string email, string oldPass, string newPass)
        {
            return await _loginRepository.ChangeUserPassword(connectionString, email, oldPass, newPass);
        }
    }
}
