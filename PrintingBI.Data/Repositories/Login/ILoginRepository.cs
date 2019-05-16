using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingBI.Data.Repositories.Login
{
    public interface ILoginRepository
    {
        bool AuthenticateUser(string connectionString, string userName, string password);
    }
}
