using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrintingBI.Data.CustomModel;
using PrintingBI.Services.AssignToAllService;

namespace PrintingBI.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/assignreportstoall")]
    [Produces("application/json")]
    public class AssignReportsToAllController : ControllerBase
    {
        private readonly ILogger<AssignReportsToAllController> _logger;
        private readonly IAssignToAllService _assignToAllService;

        public AssignReportsToAllController(IAssignToAllService assignToAllService,ILogger<AssignReportsToAllController> logger)
        {
            _assignToAllService = assignToAllService;
            _logger = logger;
        }

        [HttpGet("GetAssignToAllReports")]
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

        [HttpPost("SaveAssignReportsToAll")]
        public async Task<ActionResult<(bool,string)>> SaveAssignReportsToAll(List<Guid> reportlist)
        {
            try
            {
                (bool, string) result = await _assignToAllService.SaveAssignReportsToAll(reportlist);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}