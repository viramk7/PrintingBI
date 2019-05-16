using PrintingBI.Data.Repositories.Login;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingBI.Services.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public bool AuthenticateUser(string connectionString, string userName, string password)
        {
            return _loginRepository.AuthenticateUser(connectionString, userName, password);
        }
    }
}
