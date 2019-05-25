using Microsoft.AspNetCore.Http;
using System.IO;

namespace PrintingBI.Common
{
    public class CSVHelper
    {
        public static string ReadCSVFile(IFormFile file)
        {
            var csvData = string.Empty;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                csvData = reader.ReadToEnd();
            }

            return csvData;
        }
    }
}
