using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace PrintingBI.Common.Configurations
{
    public class CustomerDbInfo : ICustomerDbInfo
    {
        public string DbServer { get; private set; }
        public string DbName { get; private set; }
        public string DbUser { get; private set; }
        public string DbPwd { get; private set; }
        public string PBAppId { get; private set; }
        public string PBUserName { get; private set; }
        public string PBPass { get; private set; }
        public string WorkspaceID { get; private set; }
        public string FTabName { get; private set; }
        public string FColumnName { get; }
        public string FUserColumname { get; private set; }

        public CustomerDbInfo(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext.User.Identity is ClaimsIdentity identity)
            {
                //IEnumerable<Claim> claims = identity.Claims;

                DbServer = identity.FindFirst(AuthConstants.DbServer).Value;
                DbName = identity.FindFirst(AuthConstants.DbName).Value;
                DbUser = identity.FindFirst(AuthConstants.DbUser).Value;
                DbPwd = identity.FindFirst(AuthConstants.DbPwd).Value;
                PBAppId = identity.FindFirst(AuthConstants.PBAppId).Value;
                PBUserName = identity.FindFirst(AuthConstants.PBUserName).Value;
                PBPass = identity.FindFirst(AuthConstants.PBPass).Value;
                WorkspaceID = identity.FindFirst(AuthConstants.WorkspaceID).Value;
                FTabName = identity.FindFirst(AuthConstants.FTabName).Value;
                FColumnName = identity.FindFirst(AuthConstants.FColumnName).Value;
                FUserColumname = identity.FindFirst(AuthConstants.FUserColumname).Value;
            }
        }

        public string GetCustomerDbConnectionString()
        {
            return $"User ID={DbUser};password={DbPwd};Server={DbServer};port=5432;Database={DbName};Integrated Security=true; Pooling=true";
        }
    }
}
