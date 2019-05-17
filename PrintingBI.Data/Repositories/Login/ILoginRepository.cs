using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingBI.Data.Repositories.Login
{
    public interface ILoginRepository
    {
        bool AuthenticateUser(string connectionString, string userName, string password);
        bool AuthenticateUserByEmail(string connectionString, string Email);
        string GeneratePasswordResetToken(string connectionString, string email);
        bool ResetUserPassByToken(string connectionString, string token, string password);
    }
}
