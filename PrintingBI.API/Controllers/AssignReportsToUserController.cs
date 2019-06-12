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
    [Consumes("application/json")]
    public class AssignReportsToUserController : ControllerBase
    {
        private readonly ILogger<AssignReportsToUserController> _logger;
        private readonly IAssignReportsToUserService _assignToUserService;

        public AssignReportsToUserController(IAssignReportsToUserService assignToUserService, ILogger<AssignReportsToUserController> logger)
        {
            _assignToUserService = assignToUserService;
            _logger = logger;
        }

        /// <summary>
        /// Gets all the reports assigned to this user. Also reports which are assigned to all the users
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Assigns the provided reports to user
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="reportlist"></param>
        /// <returns></returns>
        /// <remarks>
        /// If any of the report which is assigned to all users is not provided \
        /// then that report will be blocked for the user. 
        /// </remarks>
        [HttpPost("{userid}/reports")]
        public async Task<ActionResult> SaveAllReports(int userid, List<Guid> reportlist)
        {
            try
            {
                var list = await _assignToUserService.SaveAssignReportsToUser(userid, reportlist);
                if (list.Count > 0)
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