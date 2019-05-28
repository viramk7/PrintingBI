using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrintingBI.Services.PowerBIService;

namespace PrintingBI.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/powerbi")]
    [Produces("application/json")]
    public class PowerBIController : ControllerBase
    {
        private readonly IPowerBIService _service;
        private readonly ILogger<PowerBIController> _logger;

        public PowerBIController(IPowerBIService service, ILogger<PowerBIController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Gets all the users
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet("GetPowerBIReport")]
        public ActionResult GetPowerBIReport()
        {
            var list = _service.GetReportList();
            var report = _service.GetPowerBIReport("dff2c1ed-e169-4ffb-a7a2-dd3a2e71b9aa");
            return Ok();
        }
    }
}