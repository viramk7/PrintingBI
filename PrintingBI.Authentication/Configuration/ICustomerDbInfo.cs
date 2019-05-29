namespace PrintingBI.Authentication.Configuration
{
    public interface ICustomerDbInfo
    {
        string DbName { get; }
        string DbPwd { get; }
        string DbServer { get; }
        string DbUser { get; }
        string PBAppId { get; }
        string PBUserName { get; }
        string PBPass { get; }
        string WorkspaceID { get; }
        string FTabName { get; }
        string FColumnName { get; }
        string FUserColumname { get; }


        string GetCustomerDbConnectionString();
    }
}