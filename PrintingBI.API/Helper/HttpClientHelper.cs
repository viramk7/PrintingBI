using PrintingBI.API.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrintingBI.API.Helper
{
    public class HttpClientHelper<T> : IHttpClientHelper<T> where T : class
    {
        private readonly IAdminConfiguration _adminConfiguration;

        public HttpClientHelper(IAdminConfiguration adminConfiguration)
        {
            _adminConfiguration = adminConfiguration;
        }

        public T Get(string apiURL)
        {
            //string Baseurl = _adminConfiguration.AdminURL;
            string Baseurl = "http://printerbi.com:81/";
            if (!string.IsNullOrEmpty(Baseurl))
            {
                using (var client = new HttpClient())
                {
                    string messages = String.Empty;
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Accept.Clear();

                    HttpResponseMessage response = client.GetAsync(apiURL).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        T obj = response.Content.ReadAsAsync<T>().Result;
                        return obj;
                    }
                }
            }
            
            return default(T);
        }
    }
}
