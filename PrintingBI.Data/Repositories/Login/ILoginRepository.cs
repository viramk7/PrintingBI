using PrintingBI.Data.CustomModel;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.Login
{
    public interface ILoginRepository
    {
        Task<AuthenticateUserResultDto> AuthenticateUser(string connectionString, string userNameOrEmail, string password, int refreshTokenExpiry);
        Task<bool> AuthenticateUserByEmail(string connectionString, string Email);
        Task<string> GeneratePasswordResetToken(string connectionString, string email);
        Task<string> ResetUserPassByToken(string connectionString, string email, string token, string password);
        Task<bool> ChangeUserPassword(string connectionString, string email, string oldPass, string newPass);
        Task<AuthenticateUserResultDto> ValidateRefreshToken(string connectionString, string userNameOrEmail, string refreshToken, int refreshTokenExpiry);
    }
}
