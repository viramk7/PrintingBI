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
        string ResetUserPassByToken(string connectionString, string email, string token, string password);
        bool ChangeUserPassword(string connectionString, string email, string oldPass, string newPass);
    }
}
