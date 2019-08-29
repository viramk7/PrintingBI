using PrintingBI.Data.CustomModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.Login
{
    public class LoginRepository : ILoginRepository
    {
        public async Task<AuthenticateUserResultDto> AuthenticateUser(string connectionString, string userNameOrEmail, string password,int refreshTokenExpiry)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connection string not provided!");


            var printingBIDbContextFactory = new PrintingBIDbContextFactory();
            var context = printingBIDbContextFactory.Create(connectionString);

            var user = context.PrinterBIUsers
                        .FirstOrDefault(m => m.UserName.ToLower() == userNameOrEmail.ToLower() 
                                          || m.Email.ToLower() == userNameOrEmail.ToLower());

            if (user == null)
                return new AuthenticateUserResultDto { IsAuthenticated = false , IsSuperAdmin = false , IsPasswordChange = false};

            if (user.Password != password)
                return new AuthenticateUserResultDto { IsAuthenticated = false, IsSuperAdmin = false, IsPasswordChange = false };

            else
            {
                string refreshToken = Guid.NewGuid().ToString();
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryDate = DateTime.Now.AddMinutes(refreshTokenExpiry);
                context.PrinterBIUsers.Update(user);
                context.SaveChanges();

                AuthenticateUserResultDto obj = new AuthenticateUserResultDto();
                obj.IsAuthenticated = true;
                obj.RefreshToken = refreshToken;
                obj.Email = user.Email;
                obj.FullName = user.FullName;
                obj.UserName = user.UserName;
                obj.UserId = user.Id;

                if (user.DepartmentId.HasValue)
                {
                    obj.DepartmentId = user.DepartmentId.ToString();
                }

                if (user.IsSuperAdmin)
                    obj.IsSuperAdmin = true;
                else
                    obj.IsSuperAdmin = false;

                if (user.IsPassChange)
                    obj.IsPasswordChange = true;
                else
                    obj.IsPasswordChange = false;
                return obj;
            }
        }

        public async Task<bool> AuthenticateUserByEmail(string connectionString, string Email)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connection string not provided!");

            var printingBIDbContextFactory = new PrintingBIDbContextFactory();
            var context = printingBIDbContextFactory.Create(connectionString);

            bool result = context.PrinterBIUsers.Any(m => m.Email == Email);

            return result;
        }

        public async Task<string> GeneratePasswordResetToken(string connectionString, string email)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connection string not provided!");

            var printingBIDbContextFactory = new PrintingBIDbContextFactory();
            var context = printingBIDbContextFactory.Create(connectionString);

            var user = context.PrinterBIUsers.FirstOrDefault(m => m.Email == email);
            if (user == null)
                return string.Empty;

            string token = Guid.NewGuid().ToString();

            user.Token = token;
            user.TokenExpiryDate = DateTime.Now.AddHours(3);
            context.Update(user);
            await context.SaveChangesAsync();

            return token;
        }

        public async Task<string> ResetUserPassByToken(string connectionString, string email, string token, string password)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connection string not provided!");

            var printingBIDbContextFactory = new PrintingBIDbContextFactory();
            var context = printingBIDbContextFactory.Create(connectionString);

            var user = context.PrinterBIUsers.FirstOrDefault(m => m.Email == email);

            if (user != null)
            {
                if (user.Token == token && user.TokenExpiryDate > DateTime.Now)
                {
                    user.Password = password;
                    user.Token = null;
                    user.TokenExpiryDate = null;

                    context.Update(user);
                    await context.SaveChangesAsync();

                    return string.Empty;
                }
                return "Invalid Token or Token is expired";
            }
            return "Invalid Email";
        }

        public async Task<bool> ChangeUserPassword(string connectionString, string email, string oldPass, string newPass)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connection string not provided!");

            var printingBIDbContextFactory = new PrintingBIDbContextFactory();
            var context = printingBIDbContextFactory.Create(connectionString);

            var user = context.PrinterBIUsers.FirstOrDefault(m => m.Email == email && m.Password == oldPass);

            if (user != null)
            {
                user.Password = newPass;
                user.IsPassChange = true;

                context.Update(user);
                await context.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<AuthenticateUserResultDto> ValidateRefreshToken(string connectionString, string userNameOrEmail, string refreshToken,int refreshTokenExpiry)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connection string not provided!");


            var printingBIDbContextFactory = new PrintingBIDbContextFactory();
            var context = printingBIDbContextFactory.Create(connectionString);

            var user = context.PrinterBIUsers
                        .FirstOrDefault(m => m.UserName.ToLower() == userNameOrEmail.ToLower()
                                          || m.Email.ToLower() == userNameOrEmail.ToLower());

            if (user == null)
                return new AuthenticateUserResultDto { IsAuthenticated = false, IsSuperAdmin = false, IsPasswordChange = false };

            if (user.RefreshToken != refreshToken || user.RefreshTokenExpiryDate < DateTime.Now)
                return new AuthenticateUserResultDto { IsAuthenticated = false, IsSuperAdmin = false, IsPasswordChange = false };

            else
            {
                string newrefreshToken = Guid.NewGuid().ToString();
                user.RefreshToken = newrefreshToken;
                user.RefreshTokenExpiryDate = DateTime.Now.AddMinutes(refreshTokenExpiry);
                context.PrinterBIUsers.Update(user);
                context.SaveChanges();

                AuthenticateUserResultDto obj = new AuthenticateUserResultDto();
                obj.IsAuthenticated = true;
                obj.RefreshToken = newrefreshToken;

                if (user.IsSuperAdmin)
                    obj.IsSuperAdmin = true;
                else
                    obj.IsSuperAdmin = false;

                if (user.IsPassChange)
                    obj.IsPasswordChange = true;
                else
                    obj.IsPasswordChange = false;
                return obj;
            }
        }
    }
}
