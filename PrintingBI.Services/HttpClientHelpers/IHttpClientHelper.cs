using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingBI.Services.HttpClientHelpers
{
    public interface IHttpClientHelper<T> where T : class
    {
        T Get(string apiURL);
    }
}
