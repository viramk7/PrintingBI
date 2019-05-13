namespace PrintingBI.Data.Repositories.Provisioning
{
    public interface IProvisionTable
    {
        string ErrorMessage { get; }

        bool Create();
    }
}