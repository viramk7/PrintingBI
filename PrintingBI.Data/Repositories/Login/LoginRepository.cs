using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintingBI.Data.Repositories.Login
{
    public class LoginRepository : ILoginRepository
    {
        public bool AuthenticateUser(string connectionString,string userName , string password)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connection string not provided!");

            var printingBIDbContextFactory = new PrintingBIDbContextFactory();
            var context = printingBIDbContextFactory.Create(connectionString);

            bool result = context.PrinterBIUsers.Any(m => m.UserName == userName && m.Password == password);

            return result;
        }

        public bool AuthenticateUserByEmail(string connectionString, string Email)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connection string not provided!");

            var printingBIDbContextFactory = new PrintingBIDbContextFactory();
            var context = printingBIDbContextFactory.Create(connectionString);

            bool result = context.PrinterBIUsers.Any(m => m.Email == Email);

            return result;
        }

        public string GeneratePasswordResetToken(string connectionString, string email)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connection string not provided!");

            var printingBIDbContextFactory = new PrintingBIDbContextFactory();
            var context = printingBIDbContextFactory.Create(connectionString);

            var user = context.PrinterBIUsers.FirstOrDefault(m => m.Email == email);
            if (user != null)
            {
                string token = Guid.NewGuid().ToString();
                
                user.Token = token;
                user.TokenExpiryDate = DateTime.Now.AddHours(3);
                context.Update(user);
                context.SaveChanges();

                return token;
            }
            return string.Empty;
        }

        public string ResetUserPassByToken(string connectionString, string email, string token, string password)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connection string not provided!");

            var printingBIDbContextFactory = new PrintingBIDbContextFactory();
            var context = printingBIDbContextFactory.Create(connectionString);

            var user = context.PrinterBIUsers.FirstOrDefault(m => m.Email == email);

            if(user != null)
            {
                if(user.Token == token && user.TokenExpiryDate > DateTime.Now)
                {
                    user.Password = password;
                    user.Token = null;
                    user.TokenExpiryDate = null;

                    context.Update(user);
                    context.SaveChanges();

                    return string.Empty;
                }
                return "Invalid Token or Token is expired";
            }
            return "Invalid Email";
        }

        public bool ChangeUserPassword(string connectionString, string email, string oldPass, string newPass)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("connection string not provided!");

            var printingBIDbContextFactory = new PrintingBIDbContextFactory();
            var context = printingBIDbContextFactory.Create(connectionString);

            var user = context.PrinterBI_Users.FirstOrDefault(m => m.Email == email && m.Password == oldPass);

            if (user != null)
            {
                    user.Password = newPass;

                    context.Update(user);
                    context.SaveChanges();

                    return true;
            }
            return false;
        }
    }
}
