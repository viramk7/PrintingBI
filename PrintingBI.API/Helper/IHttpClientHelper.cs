using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrintingBI.API.Helper
{
    public interface IHttpClientHelper<T> where T : class
    {
        T Get(string apiURL);
    }
}
