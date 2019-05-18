using PrintingBI.Services.AdminConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrintingBI.Services.HttpClientHelpers
{
    public class HttpClientHelper<T> : IHttpClientHelper<T> where T : class
    {
        private readonly IAdminConfiguration _adminConfiguration;

        public HttpClientHelper(IAdminConfiguration adminConfiguration)
        {
            _adminConfiguration = adminConfiguration;
        }

        public async Task<T> Get(string apiURL)
        {
            string Baseurl = _adminConfiguration.AdminURL;

            if (!string.IsNullOrEmpty(Baseurl))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Accept.Clear();

                    HttpResponseMessage response = client.GetAsync(apiURL).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        T obj = await response.Content.ReadAsAsync<T>();
                        return obj;
                    }
                }
            }

            return default(T);
        }
    }
}
