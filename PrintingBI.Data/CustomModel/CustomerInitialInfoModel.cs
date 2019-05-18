using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingBI.Data.CustomModel
{
    public class CustomerInitialInfoModel
    {
        public string ApplicationId { get; set; }

        public string PowerBIUserName { get; set; }

        public string PowerBIUserPass { get; set; }

        public string WorkSpaceId { get; set; }

        public string TenantUniqueId { get; set; }

        public string TenantDBServer { get; set; }

        public string TenantDBUser { get; set; }

        public string TenantDBPassword { get; set; }

        public string TenantDBName { get; set; }

        public string FilterTableName { get; set; }

        public string FilterColumnName { get; set; }

        public string FilterUserColumnName { get; set; }

        public string ConnectionString
        {
            get
            {
                return $"User ID={TenantDBUser};password={TenantDBPassword};Server={TenantDBServer};port=5432;Database={TenantDBName};Integrated Security=true; Pooling=true";
            }
        }

    }
}

