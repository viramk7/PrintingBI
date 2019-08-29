using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrintingBI.API.Models;
using PrintingBI.Common;
using PrintingBI.Data.Entities;
using PrintingBI.Services.PowerBIService;
using PrintingBI.Services.ReportsService;

namespace PrintingBI.API.Controllers
{
    [ApiController]
    [Route("api/printerbi/reports")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class ReportMasterController : ControllerBase
    {
        private readonly IReportMasterService _service;
        private readonly ILogger<ReportMasterController> _logger;

        public ReportMasterController(IReportMasterService service, ILogger<ReportMasterController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Returns all the reports in database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<ReportMasterCustomModel>>> GetAllReports()
        {
            try
            {
                var list = await _service.GetAllReports();
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Syncs the reports with PowerBI
        /// </summary>
        /// <returns></returns>
        [HttpGet("Sync")]
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

        [HttpGet("{id}")]
        public async Task<ActionResult<PBReportViewModel>> GetSingleReport(string id)
        {
            try
            {
                return await _service.GetSingleReportData(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}