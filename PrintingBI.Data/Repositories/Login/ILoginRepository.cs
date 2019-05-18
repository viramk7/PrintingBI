using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.Login
{
    public interface ILoginRepository
    {
        Task<bool> AuthenticateUser(string connectionString, string userName, string password);
        Task<bool> AuthenticateUserByEmail(string connectionString, string Email);
        Task<string> GeneratePasswordResetToken(string connectionString, string email);
        Task<string> ResetUserPassByToken(string connectionString, string email, string token, string password);
        Task<bool> ChangeUserPassword(string connectionString, string email, string oldPass, string newPass);
    }
}
