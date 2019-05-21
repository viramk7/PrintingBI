using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrintingBI.API.Models;
using PrintingBI.Services.Departments;
using PrintingBI.Services.ProvisionPowerBITenants;
using PrintingBI.Services.Users;
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
        private readonly IUserService _userService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IProvisionPowerBITenantsService provisionPowerBITenantsService,
                               IDepartmentService departmentService, IUserService userService,
                               ILogger<AdminController> logger)
        {
            _provisionPowerBITenantsService = provisionPowerBITenantsService;
            _departmentService = departmentService;
            _userService = userService;
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

        [HttpPost("DeProvisionPowerBITenants")]
        public async Task<ActionResult> DeProvisionPowerBITenants(CustomerDbCredsInputDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var (succeed, message) = await
                _provisionPowerBITenantsService.DeProvision(model.ConnectionString);

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

        [HttpPost("InsertUsers")]
        public async Task<ActionResult> InsertUsers([FromForm]InsertUserInputDto model)
        {
            try
            {
                await _userService.Insert(model.ConnectionString, model.UserFile);
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
