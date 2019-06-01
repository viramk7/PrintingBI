using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrintingBI.API.Models;
using PrintingBI.Services.AdminTenantService;
using PrintingBI.Services.Departments;
using PrintingBI.Services.ProvisionPowerBITenants;
using PrintingBI.Services.Users;
using System;
using System.Threading.Tasks;

namespace PrintingBI.API.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class AdminController : ControllerBase
    {
        private readonly IProvisionPowerBITenantsService _provisionPowerBITenantsService;
        private readonly IDepartmentService _departmentService;
        private readonly IUserService _userService;
        private readonly IAdminTenantService _adminService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IProvisionPowerBITenantsService provisionPowerBITenantsService,
                               IDepartmentService departmentService, IUserService userService,
                               IAdminTenantService adminService,
                               ILogger<AdminController> logger)
        {
            _provisionPowerBITenantsService = provisionPowerBITenantsService;
            _departmentService = departmentService;
            _userService = userService;
            _adminService = adminService;
            _logger = logger;
        }

        /// <summary>
        /// This api provisions the power BI tenants into the system.
        /// The respective tables and configurations will be created in the db provided in the input.
        /// By Default 1 admin user will be created as below
        /// UserName = "admin",
        /// Email = "admin@gmail.com",
        /// Password = "12345"
        /// </summary>
        /// <param name="model">Database credentials to provision tenant</param>
        /// <returns></returns>
        [HttpPost("ProvisionPowerBITenants")]
        public async Task<ActionResult> ProvisionPowerBITenants(CustomerDbCredsInputDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var (succeed, message) = await
                _provisionPowerBITenantsService.Provision(model.GetConnectionString());

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

        /// <summary>
        /// This api deprovisions all the configurations and removes all the tables and data from db
        /// </summary>
        /// <param name="model">Dababase credentials to deprovision</param>
        /// <returns></returns>
        [HttpPost("DeProvisionPowerBITenants")]
        public async Task<ActionResult> DeProvisionPowerBITenants(CustomerDbCredsInputDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var (succeed, message) = await
                _provisionPowerBITenantsService.DeProvision(model.GetConnectionString());

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

        /// <summary>
        /// This api is inserts the departments once the tenant is provisioned.
        /// To insert the departments upload the file with department name and parent dept name
        /// </summary>
        /// <param name="model">Db credentials for the tenant and department file(.csv)</param>
        /// <returns></returns>
        [HttpPost("InsertDepartments")]
        public async Task<ActionResult> InsertDepartments([FromForm]InsertDepartmentsInputDto model)
        {
            try
            {
                var (isFileValid, message) = await
                    _departmentService.Insert(model.GetConnectionString(), model.DepartmentFile);

                if (!isFileValid)
                {
                    ModelState.AddModelError("", message);
                    return BadRequest(ModelState);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// This api creates the user and maps them to departments and role rights
        /// Departments should exists before inserting users. 
        /// File with users needs to be uploaded in order to insert the users
        /// </summary>
        /// <param name="model">Db credentials for the tenant and user file(.csv)</param>
        /// <returns></returns>
        [HttpPost("InsertUsers")]
        public async Task<ActionResult> InsertUsers([FromForm]InsertUserInputDto model)
        {
            try
            {
                var (isSuccess, message) = await _userService.Insert(model.GetConnectionString(), model.UserFile);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", message);
                    return BadRequest(ModelState);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// this api is used to validate the host name of customer
        /// </summary>
        /// <param name="model">Host name to be validated</param>
        /// <returns></returns>
        [HttpPost("ValidateCustomerTenant")]
        [ProducesResponseTypeAttribute(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ValidateCustomerTenant(ValidateTenantDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var intialInfo = await _adminService.GetCustomerInialInfo(model.HostName);
                if (intialInfo != null && !string.IsNullOrEmpty(intialInfo.TenantDBServer))
                {
                    return Ok(new
                    {
                        Valid = true,
                        Info = intialInfo
                    });
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}
