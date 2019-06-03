using PrintingBI.Data.CustomModel;
using System.Threading.Tasks;

namespace PrintingBI.Services.LoginService
{
    public interface ILoginService
    {
        Task<AuthenticateUserResultDto> AuthenticateUser(string connectionString, string userNameOrEmail, string password);
        Task<bool> AuthenticateUserByEmail(string connectionString, string Email);
        Task<string> GeneratePasswordResetToken(string connectionString, string email);
        Task<string> ResetUserPassByToken(string connectionString, string email, string token, string password);
        Task<bool> ChangeUserPassword(string connectionString, string email, string oldPass, string newPass);
        void SendForgotPasswordEmail(string token, string emailaddress);
    }
}
