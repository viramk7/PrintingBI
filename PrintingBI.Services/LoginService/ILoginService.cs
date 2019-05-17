using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingBI.Services.LoginService
{
    public interface ILoginService
    {
        bool AuthenticateUser(string connectionString, string userName, string password);
        bool AuthenticateUserByEmail(string connectionString, string Email);
        string GeneratePasswordResetToken(string connectionString, string email);
        bool ResetUserPassByToken(string connectionString, string token, string password);
    }
}
