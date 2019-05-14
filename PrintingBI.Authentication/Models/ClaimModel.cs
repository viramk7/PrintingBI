using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrintingBI.API.Models
{
    public class ClaimModel
    {
        public ClaimModel(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public string Value { get; set; }
    }
}
