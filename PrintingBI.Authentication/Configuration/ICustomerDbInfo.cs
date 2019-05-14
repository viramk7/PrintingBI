namespace PrintingBI.Authentication.Configuration
{
    public interface ICustomerDbInfo
    {
        string DbName { get; }
        string DbPwd { get; }
        string DbServer { get; }
        string DbUser { get; }

        string GetCustomerDbConnectionString();
    }
}