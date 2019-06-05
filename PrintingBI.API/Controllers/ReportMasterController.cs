using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrintingBI.API.Models;
using PrintingBI.Data.Entities;
using PrintingBI.Services.PowerBIService;
using PrintingBI.Services.ReportsService;

namespace PrintingBI.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/reportmaster")]
    [Produces("application/json")]
    public class ReportMasterController : ControllerBase
    {
        private readonly IReportMasterService _service;
        private readonly ILogger<ReportMasterController> _logger;

        public ReportMasterController(IReportMasterService service, ILogger<ReportMasterController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetAllReports")]
        public async Task<ActionResult<List<PrinterBIReportMaster>>> GetAllReports()
        {
            try
            {
                var list = await _service.GetAllReports();
                return list;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        [HttpGet("SyncReports")]
        public async Task<ActionResult> SyncReports()
        {
            try
            {
                await _service.SyncReports();
                return StatusCode(StatusCodes.Status200OK, "Sync reports successfuully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        [HttpGet("GetSingleReport")]
        public async Task<ActionResult<PBReportViewModel>> GetSingleReport(string reportId)
        {
            try
            {
                return await _service.GetSingleReportData(reportId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}