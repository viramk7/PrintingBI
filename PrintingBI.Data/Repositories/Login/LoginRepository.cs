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

            bool result = context.Users.Any(m => m.Name == userName && m.Password == password);

            return result;
        }

        //public bool AuthenticateUserByEmail(string connectionString , string Email)
        //{
        //    if (string.IsNullOrEmpty(connectionString))
        //        throw new ArgumentException("connection string not provided!");

        //    var printingBIDbContextFactory = new PrintingBIDbContextFactory();
        //    var context = printingBIDbContextFactory.Create(connectionString);

        //    bool result = context.Users.Any(m => m.Email == Email);

        //    return result;
        //}

        //public string GeneratePasswordResetToken(string connectionString, string email)
        //{
        //    if (string.IsNullOrEmpty(connectionString))
        //        throw new ArgumentException("connection string not provided!");

        //    var printingBIDbContextFactory = new PrintingBIDbContextFactory();
        //    var context = printingBIDbContextFactory.Create(connectionString);

        //    bool result = context.Users.Any(m => m.Email == email);
        //    if (result)
        //    {
        //        var user = context.Users.FirstOrDefault(m => m.Email == email);
        //    }
        //    return string.Empty;
        //}
    }
}
