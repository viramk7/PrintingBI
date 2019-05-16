using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingBI.Services.LoginService
{
    public interface ILoginService
    {
        bool AuthenticateUser(string connectionString, string userName, string password);
    }
}
