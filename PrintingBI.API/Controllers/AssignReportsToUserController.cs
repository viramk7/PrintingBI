using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrintingBI.Data.CustomModel;
using PrintingBI.Services.AssignReportsToUser;

namespace PrintingBI.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    [Produces("application/json")]
    public class AssignReportsToUserController : ControllerBase
    {
        private readonly ILogger<AssignReportsToUserController> _logger;
        private readonly IAssignReportsToUserService _assignToUserService;

        public AssignReportsToUserController(IAssignReportsToUserService assignToUserService, ILogger<AssignReportsToUserController> logger)
        {
            _assignToUserService = assignToUserService;
            _logger = logger;
        }

        [HttpGet("{userid}/reports")]
        public async Task<ActionResult<List<AssignToUserReportDto>>> GetAllReports(int userid)
        {
            try
            {
                var list = await _assignToUserService.GetAllReportsAssignToUser(userid);
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        [HttpPost("{userid}/reports")]
        public async Task<ActionResult> SaveAllReports(int userid,List<Guid> reportlist)
        {
            try
            {
                var list = await _assignToUserService.SaveAssignReportsToUser(userid,reportlist);
                if(list.Count > 0)
                {
                    return NotFound(list);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}