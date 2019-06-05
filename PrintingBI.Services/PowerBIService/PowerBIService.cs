using Microsoft.PowerBI.Api.V2;
using Microsoft.PowerBI.Api.V2.Models;
using Microsoft.Rest;
using Newtonsoft.Json.Linq;
using PrintingBI.Common.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PrintingBI.Services.PowerBIService
{
    public class PowerBIService : IPowerBIService
    {
        private const string aadTokenGenerationEndpoint = "https://login.windows.net/common/oauth2/token";
        private const string resourceUriPowerBi = "https://analysis.windows.net/powerbi/api";
        private const string urlPowerBiRestApiRoot = "https://api.powerbi.com/";

        private static string PBIApplicationId = string.Empty;
        private static string PBIUserName = string.Empty;
        private static string PBIUserPassword = string.Empty;
        private static string PBIWorkspaceId = string.Empty;
        private readonly ICustomerDbInfo _customerDbInfo;

        public PowerBIService(ICustomerDbInfo customerDbInfo)
        {
            _customerDbInfo = customerDbInfo;
            PBIApplicationId = _customerDbInfo.PBAppId;
            PBIUserName = _customerDbInfo.PBUserName;
            PBIUserPassword = _customerDbInfo.PBPass;
            PBIWorkspaceId = _customerDbInfo.WorkspaceID;

            //PBIApplicationId = "bd853607-9d2d-42ac-8764-62987a571866";
            //PBIUserName = "powerbi.gateway@printerbi.com";
            //PBIUserPassword = "Equitrac2018.";
            //PBIWorkspaceId = "5f13b48f-bc94-42a0-b7d1-cccf40303ed8";
        }

        private static async Task<TokenCredentials> GetAccessToken()
        {
            using (HttpClient client = new HttpClient())
            {
                var accept = "application/json";

                client.DefaultRequestHeaders.Add("Accept", accept);
                string postBody = null;

                postBody = $@"resource=https%3A%2F%2Fanalysis.windows.net/powerbi/api
                        &client_id={PBIApplicationId}
                        &grant_type=password
                        &username={PBIUserName}
                        &password={PBIUserPassword}
                        &scope=openid";

                var tokenResult = await client.PostAsync(aadTokenGenerationEndpoint, new StringContent(postBody, Encoding.UTF8, "application/x-www-form-urlencoded"));
                tokenResult.EnsureSuccessStatusCode();
                var tokenData = await tokenResult.Content.ReadAsStringAsync();

                JObject parsedTokenData = JObject.Parse(tokenData);

                var token = parsedTokenData["access_token"].Value<string>();
                return new TokenCredentials(token, "Bearer");
            }
        }

        private static PowerBIClient GetPowerBiClient()
        {
            var tokenCredentials = GetAccessToken().Result;
            //var tokenCredentials = new TokenCredentials(GetAccessToken(), "Bearer");
            return new PowerBIClient(new Uri(urlPowerBiRestApiRoot), tokenCredentials);
        }

        public List<Report> GetReportList()
        {
            var client = GetPowerBiClient();
            var reports = client.Reports.GetReportsInGroupAsync(PBIWorkspaceId).Result.Value;
            var list = reports.ToList();
            return list;
        }

        public PBReportViewModel GetPowerBIReport(string reportId)
        {
            var client = GetPowerBiClient();
            var reports = client.Reports.GetReportsInGroupAsync(PBIWorkspaceId).Result.Value;
            Report report = reports.Where(r => r.Id == reportId).FirstOrDefault();

            var generateTokenRequestParameters = new GenerateTokenRequest(accessLevel: "view", allowSaveAs: true);

            var dataset = client.Datasets.GetDatasetByIdInGroupAsync(PBIWorkspaceId, report.DatasetId).Result;

            var token = client.Reports.GenerateTokenInGroupAsync(PBIWorkspaceId, report.Id, generateTokenRequestParameters).Result;

            var embedConfig = new EmbedConfiguration()
            {
                EmbedToken = token,
                EmbedUrl = report.EmbedUrl,
                Id = report.Id
            };
            return new PBReportViewModel { Report = report, EmbedConfig = embedConfig };

            //if (!string.IsNullOrEmpty(SettingConfig.FilterTableName) && !string.IsNullOrEmpty(SettingConfig.FilterColumnName) && !string.IsNullOrEmpty(SettingConfig.FilterUserColumnName) && !SessionHelper.IsSuperAdmin)
            //{
            //    string filter = string.Empty;
            //    if (SessionHelper.RoleRightsId > 0)
            //    {
            //        if (SessionHelper.DepartmentId == SessionHelper.RoleRightsId)
            //        {
            //            filter = "&$filter=" + SettingConfig.FilterTableName + "/" + SettingConfig.FilterColumnName + " eq '" + SessionHelper.DepartmentName + "'";
            //            //filter = "&$filter=" + SettingConfig.FilterTableName + "/" + SettingConfig.FilterColumnName + " eq 'activo fijo'";
            //        }
            //        else
            //        {
            //            filter = "&$filter=" + SettingConfig.FilterTableName + "/" + SettingConfig.FilterColumnName + " in ('" + SessionHelper.DepartmentHierarchy + "')";
            //        }
            //    }
            //    else
            //    {
            //        filter = "&$filter=" + SettingConfig.FilterTableName + "/" + SettingConfig.FilterUserColumnName + " eq '" + SessionHelper.WelcomeUser + "'";
            //    }

            //    var embedConfig = new EmbedConfiguration()
            //    {
            //        EmbedToken = token,
            //        //EmbedUrl = report.EmbedUrl + "&$filter=" + SettingConfig.FilterTableName + "/" + SettingConfig.FilterColumnName + " in ('" + SessionHelper.DepartmentHierarchy + "')",
            //        EmbedUrl = report.EmbedUrl + filter,
            //        //EmbedUrl = report.EmbedUrl + "&$filter=" + SettingConfig.FilterTableName.ToUpper() + "/" + SettingConfig.FilterColumnName + " in ('proyectos','gestion corporativa','gestion de servicios')",
            //        Id = report.Id
            //    };
            //    return new ReportViewModel { Report = report, EmbedConfig = embedConfig };
            //}
            //else
            //{
            //    var embedConfig = new EmbedConfiguration()
            //    {
            //        EmbedToken = token,
            //        EmbedUrl = report.EmbedUrl /*+ "&$filter=VIEW_REPORT_EQCAS/DepartmentName in ('td100001','td110001')"*/,
            //        Id = report.Id
            //    };
            //    return new ReportViewModel { Report = report, EmbedConfig = embedConfig };
            //}
        }
    }
}
