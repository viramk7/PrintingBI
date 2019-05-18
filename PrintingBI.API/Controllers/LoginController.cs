using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrintingBI.API.Configuration;
using PrintingBI.API.Models;
using PrintingBI.Authentication;
using PrintingBI.Authentication.Models.Dtos;
using PrintingBI.Data.CustomModel;
using PrintingBI.Services.AdminTenantService;
using PrintingBI.Services.LoginService;

namespace PrintingBI.API.Controllers
{
    [Route("api/login")]
    [ApiController]
    [Produces("application/json")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IAdminTenantService _adminService;
        private readonly ILoginService _loginService;
        private readonly IJwtConfiguration _jwtConfiguration;

        public LoginController(ILogger<LoginController> logger, IAdminTenantService adminService, ILoginService loginService, IJwtConfiguration jwtConfiguration)
        {
            _logger = logger;
            _adminService = adminService;
            _loginService = loginService;
            _jwtConfiguration = jwtConfiguration;
        }

        /// <summary>
        /// It will checks whether user credentials are valid for the particular domain/host 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AuthenticateLoginUser")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CustomerInitialInfoModel>> AuthenticateLoginUser(LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                CustomerInitialInfoModel intialInfo = await _adminService.GetCustomerInialInfo(model.HostName);

                if (intialInfo != null && !string.IsNullOrEmpty(intialInfo.TenantDBServer))
                {
                    //: TODO : Change static DB info , Change staitc Base url from HttpClientHelper

                    intialInfo.TenantDBServer = "74.208.24.39";
                    intialInfo.TenantDBName = "PrintingBI-Customer1";
                    intialInfo.TenantDBUser = "postgres";
                    intialInfo.TenantDBPassword = "Printerbi@.";

                    bool result = await _loginService.AuthenticateUser(intialInfo.ConnectionString, model.UserName, model.Password);
                    if (result)
                    {
                        var claims = new List<ClaimModel>
                                {
                                    new ClaimModel(AuthConstants.DbServer,intialInfo.TenantDBServer),
                                    new ClaimModel(AuthConstants.DbName,intialInfo.TenantDBName),
                                    new ClaimModel(AuthConstants.DbUser,intialInfo.TenantDBUser),
                                    new ClaimModel(AuthConstants.DbPwd,intialInfo.TenantDBPassword),
                                };

                        var token = TokenBuilder.CreateJsonWebToken(
                                    model.UserName,
                                    claims,
                                    _jwtConfiguration.Audience,
                                    _jwtConfiguration.Issuer,
                                    Guid.NewGuid(),
                                    DateTime.UtcNow.AddMinutes(Convert.ToInt32(_jwtConfiguration.ExpireTime)));

                        return intialInfo;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid UserName and Password.");
                        return BadRequest(ModelState);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Host Name.");
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Forgot Password API for User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("ForgotPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ForgotPassword(ForgotPassDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                CustomerInitialInfoModel intialInfo = await _adminService.GetCustomerInialInfo(model.HostName);

                if (intialInfo != null && !string.IsNullOrEmpty(intialInfo.TenantDBServer))
                {
                    intialInfo.TenantDBServer = "74.208.24.39";
                    intialInfo.TenantDBName = "PrintingBI-Customer1";
                    intialInfo.TenantDBUser = "postgres";
                    intialInfo.TenantDBPassword = "Printerbi@.";

                    bool user = await _loginService.AuthenticateUserByEmail(intialInfo.ConnectionString, model.EmailAddress);
                    if (!user)
                        return StatusCode(StatusCodes.Status404NotFound, "Email address in not valid.");

                    var token = _loginService.GeneratePasswordResetToken(intialInfo.ConnectionString, model.EmailAddress);
                    return Ok(token);
                }

                // TODO: Send an email to the user with token

                return StatusCode(StatusCodes.Status401Unauthorized, "Request is not Unthorized.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Reset Password with token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("ResetPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> ResetPassword(ResetPassDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                CustomerInitialInfoModel intialInfo = await _adminService.GetCustomerInialInfo(model.HostName);

                if (intialInfo != null && !string.IsNullOrEmpty(intialInfo.TenantDBServer))
                {
                    intialInfo.TenantDBServer = "74.208.24.39";
                    intialInfo.TenantDBName = "PrintingBI-Customer1";
                    intialInfo.TenantDBUser = "postgres";
                    intialInfo.TenantDBPassword = "Printerbi@.";

                    string userResult = await _loginService.ResetUserPassByToken(intialInfo.ConnectionString, model.Email, model.Token, model.Password);
                    if (!string.IsNullOrEmpty(userResult))
                        return StatusCode(StatusCodes.Status204NoContent, userResult);
                    else
                        return StatusCode(StatusCodes.Status200OK, "Password Reset Successfully.");

                }

                return StatusCode(StatusCodes.Status401Unauthorized, "Request is not Unthorized.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }


        /// <summary>
        /// Change User password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("ChangePassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> ChangePassword(ChangePassDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                CustomerInitialInfoModel intialInfo = await _adminService.GetCustomerInialInfo(model.HostName);

                if (intialInfo != null && !string.IsNullOrEmpty(intialInfo.TenantDBServer))
                {
                    intialInfo.TenantDBServer = "74.208.24.39";
                    intialInfo.TenantDBName = "PrintingBI-Customer1";
                    intialInfo.TenantDBUser = "postgres";
                    intialInfo.TenantDBPassword = "Printerbi@.";

                    bool result = await _loginService.ChangeUserPassword(intialInfo.ConnectionString, model.Email, model.OldPassword, model.NewPassword);

                    if (!result)
                        return StatusCode(StatusCodes.Status401Unauthorized, "Invalid Email & old password.");
                    else
                        return StatusCode(StatusCodes.Status200OK, "Password Changed Successfully.");

                }

                return StatusCode(StatusCodes.Status401Unauthorized, "Request is not Unthorized.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// API used to validate host name of customer
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("ValidateCustomerTenant")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                        valid = true,
                        info = intialInfo
                    });
                }
                
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}