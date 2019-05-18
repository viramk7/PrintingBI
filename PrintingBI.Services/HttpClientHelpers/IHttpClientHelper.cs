using System.Threading.Tasks;

namespace PrintingBI.Services.HttpClientHelpers
{
    public interface IHttpClientHelper<T> where T : class
    {
        Task<T> Get(string apiURL);
    }
}
