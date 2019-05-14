using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.Provisioning
{
    public interface IProvisionTable
    {
        string ErrorMessage { get; }

        Task<bool> Provision();
    }
}