using System.Threading.Tasks;

namespace PrintingBI.Data.Repositories.Provisioning
{
    public interface IProvision
    {
        string ErrorMessage { get; }

        Task<bool> Provision();
    }
}