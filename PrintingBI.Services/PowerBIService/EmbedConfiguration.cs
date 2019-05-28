using Microsoft.PowerBI.Api.V2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingBI.Services.PowerBIService
{
    public class EmbedConfiguration
    {
        public string Id { get; set; }
        public string DatasetId { get; set; }
        public string EmbedUrl { get; set; }
        public EmbedToken EmbedToken { get; set; }
        public int MinutesToExpiration
        {
            get
            {
                var minutesToExpiration = EmbedToken.Expiration.Value - DateTime.UtcNow;
                return minutesToExpiration.Minutes;
            }
        }
        public string ErrorMessage { get; internal set; }
    }
}
