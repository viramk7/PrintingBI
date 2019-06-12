using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrintingBI.Common;
using PrintingBI.Data.CustomModel;
using PrintingBI.Services.AssignToAllService;

namespace PrintingBI.API.Controllers
{
    [ApiController]
    [Route("api/reportsassignedtoall")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize(Roles = RoleModel.SuperAdmin)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class AssignReportsToAllController : ControllerBase
    {
        private readonly ILogger<AssignReportsToAllController> _logger;
        private readonly IAssignToAllService _assignToAllService;

        public AssignReportsToAllController(IAssignToAllService assignToAllService, ILogger<AssignReportsToAllController> logger)
        {
            _assignToAllService = assignToAllService;
            _logger = logger;
        }

        /// <summary>
        /// Gets all the reports with a flag isAssignedToAllUsers if assigned to all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<AssignToAllReportDto>>> GetAssignToAllReports()
        {
            try
            {
                var list = await _assignToAllService.GetAssignToAllReports();
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Assigns the provided reports to all the users
        /// </summary>
        /// <param name="reportlist">ex: ["reportid1","reportid2"]</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> SaveAssignReportsToAll(List<Guid> reportlist)
        {
            try
            {
                string result = await _assignToAllService.SaveAssignReportsToAll(reportlist);
                if (string.IsNullOrEmpty(result))
                {
                    return Ok();
                }
                else
                {
                    return NotFound(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}