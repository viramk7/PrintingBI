﻿using Microsoft.PowerBI.Api.V2.Models;

namespace PrintingBI.Services.PowerBIService
{
    public class PBReportViewModel
    {
        public Report Report { get; set; }
        public EmbedConfiguration EmbedConfig { get; set; }
        public ReportMode ReportMode { get; set; }
        public DatasetViewModel CurrentDataset { get; set; }
    }

    public enum ReportMode
    {
        NoReport,
        ExistingReport,
        NewReport
    }
}
