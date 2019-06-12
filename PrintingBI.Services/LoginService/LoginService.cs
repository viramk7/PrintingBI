using PrintingBI.Data.CustomModel;
using PrintingBI.Data.Repositories.Login;
using PrintingBI.Services.AdminConfiguration;
using PrintingBI.Services.Notification;
using System.Threading.Tasks;

namespace PrintingBI.Services.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IEmailNotificationService _emailNotificationService;
        private readonly IAdminConfiguration _adminConfiguration;

        public LoginService(ILoginRepository loginRepository, IEmailNotificationService emailNotificationService, IAdminConfiguration adminConfiguration)
        {
            _loginRepository = loginRepository;
            _emailNotificationService = emailNotificationService;
            _adminConfiguration = adminConfiguration;
        }

        public async Task<AuthenticateUserResultDto> AuthenticateUser(string connectionString, string userNameOrEmail, string password, int refreshTokenExpiry)
        {
            return await _loginRepository.AuthenticateUser(connectionString, userNameOrEmail, password,refreshTokenExpiry);
        }

        public async Task<bool> AuthenticateUserByEmail(string connectionString, string Email)
        {
            return await _loginRepository.AuthenticateUserByEmail(connectionString, Email);
        }

        public async Task<string> GeneratePasswordResetToken(string connectionString, string email)
        {
            return await _loginRepository.GeneratePasswordResetToken(connectionString, email);
        }

        public async Task<string> ResetUserPassByToken(string connectionString, string email, string token, string password)
        {
            return await _loginRepository.ResetUserPassByToken(connectionString, email, token, password);
        }

        public async Task<bool> ChangeUserPassword(string connectionString, string email, string oldPass, string newPass)
        {
            return await _loginRepository.ChangeUserPassword(connectionString, email, oldPass, newPass);
        }

        public async void SendForgotPasswordEmail(string token, string emailaddress)
        {
            string resetPassUrl = $"{_adminConfiguration.FrontEndResetPassURL}?email={emailaddress}&Token={token}";
            
            var bodyTemplate = await System.IO.File.ReadAllTextAsync("Template/ForgotPassword.html");

            bodyTemplate = bodyTemplate.Replace("[@PORTAL-NAME]", "Printer BI System");
            bodyTemplate = bodyTemplate.Replace("[@FULLNAME]", emailaddress);
            bodyTemplate = bodyTemplate.Replace("[@LINK]", resetPassUrl);

            _emailNotificationService.SendAsyncEmail(emailaddress, "Forgot Password Link", bodyTemplate, true);

        }

        public async Task<AuthenticateUserResultDto> ValidateRefreshToken(string connectionString, string userNameOrEmail, string refreshToken, int refreshTokenExpiry)
        {
            return await _loginRepository.ValidateRefreshToken(connectionString, userNameOrEmail, refreshToken, refreshTokenExpiry);
        }
    }
}
