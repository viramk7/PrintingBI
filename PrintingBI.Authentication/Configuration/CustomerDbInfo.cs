using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PrintingBI.Authentication.Configuration
{
    public class CustomerDbInfo : ICustomerDbInfo
    {
        public string DbServer { get; private set; }
        public string DbName { get; private set; }
        public string DbUser { get; private set; }
        public string DbPwd { get; private set; }

        public CustomerDbInfo(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext.User.Identity is ClaimsIdentity identity)
            {
                //IEnumerable<Claim> claims = identity.Claims;

                DbServer = identity.FindFirst(AuthConstants.DbServer).Value;
                DbName = identity.FindFirst(AuthConstants.DbName).Value;
                DbUser = identity.FindFirst(AuthConstants.DbUser).Value;
                DbPwd = identity.FindFirst(AuthConstants.DbPwd).Value;
            }
        }

        public string GetCustomerDbConnectionString()
        {
            return $"User ID={DbUser};password={DbPwd};Server={DbServer};port=5432;Database={DbName};Integrated Security=true; Pooling=true";
        }
    }
}
