using Microsoft.PowerBI.Api.V2.Models;

namespace PrintingBI.Services.PowerBIService
{
    public class DatasetViewModel
    {
        public Dataset dataset { get; set; }
        public EmbedConfiguration EmbedConfig { get; set; }
    }
}
