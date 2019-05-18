using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrintingBI.API.Models;
using PrintingBI.Services.Departments;
using PrintingBI.Services.ProvisionPowerBITenants;
using System;
using System.Threading.Tasks;

namespace PrintingBI.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Produces("application/json")]
    public class AdminController : ControllerBase
    {
        private readonly IProvisionPowerBITenantsService _provisionPowerBITenantsService;
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IProvisionPowerBITenantsService provisionPowerBITenantsService,
                               IDepartmentService departmentService,
                               ILogger<AdminController> logger)
        {
            _provisionPowerBITenantsService = provisionPowerBITenantsService;
            _departmentService = departmentService;
            _logger = logger;
        }

        [HttpPost("ProvisionPowerBITenants")]
        public async Task<ActionResult> ProvisionPowerBITenants(CustomerDbCredsInputDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var (succeed, message) = await
                _provisionPowerBITenantsService.Provision(model.ConnectionString);

                if (succeed)
                    return Ok();
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        [HttpPost("InsertDepartments")]
        public async Task<ActionResult> InsertDepartments([FromForm]InsertDepartmentsInputDto model)
        {
            try
            {
                await _departmentService.Insert(model.ConnectionString, model.DepartmentFile);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
