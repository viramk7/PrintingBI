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
using PrintingBI.Services.Notification;

namespace PrintingBI.API.Controllers
{
    [ApiController]
    [Route("api/login")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IAdminTenantService _adminService;
        private readonly ILoginService _loginService;
        private readonly IJwtConfiguration _jwtConfiguration;
        private readonly IEmailNotificationService _emailNotificationService;

        public LoginController(ILogger<LoginController> logger, 
                              IAdminTenantService adminService, 
                              ILoginService loginService, 
                              IJwtConfiguration jwtConfiguration,
                              IEmailNotificationService emailNotificationService)
        {
            _logger = logger;
            _adminService = adminService;
            _loginService = loginService;
            _jwtConfiguration = jwtConfiguration;
            _emailNotificationService = emailNotificationService;
        }

        /// <summary>
        /// Authenticates given user for provided hostname
        /// </summary>
        /// <param name="model">Provide the hostname, username or email and password</param>
        /// <returns></returns>
        [HttpPost("AuthenticateUser")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AuthenticateUserOutputDto>> AuthenticateUser(LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var intialInfo = await GetTenantDbInfo(model.HostName);
                if (intialInfo == null || string.IsNullOrEmpty(intialInfo.TenantDBServer))
                {
                    ModelState.AddModelError("", "Invalid Host Name.");
                    return BadRequest(ModelState);
                }

                var result = await _loginService.AuthenticateUser(intialInfo.GetConnectionString(), model.UserNameOrEmail, model.Password);
                if (!result.IsAuthenticated)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Invalid UserName and Password.");
                }

                var claims = new List<ClaimModel>
                {
                    new ClaimModel(AuthConstants.DbServer,intialInfo.TenantDBServer),
                    new ClaimModel(AuthConstants.DbName,intialInfo.TenantDBName),
                    new ClaimModel(AuthConstants.DbUser,intialInfo.TenantDBUser),
                    new ClaimModel(AuthConstants.DbPwd,intialInfo.TenantDBPassword),
                    new ClaimModel(AuthConstants.PBAppId,intialInfo.ApplicationId),
                    new ClaimModel(AuthConstants.PBUserName,intialInfo.PowerBIUserName),
                    new ClaimModel(AuthConstants.PBPass,intialInfo.PowerBIUserPass),
                    new ClaimModel(AuthConstants.WorkspaceID,intialInfo.WorkSpaceId),
                    //new ClaimModel(AuthConstants.IsSuperAdmin.ToString(),result.IsSuperAdmin),
                    new ClaimModel(AuthConstants.FTabName,string.IsNullOrEmpty(intialInfo.FilterTableName) ? "" : intialInfo.FilterTableName),
                    new ClaimModel(AuthConstants.FColumnName,string.IsNullOrEmpty(intialInfo.FilterColumnName) ? "" : intialInfo.FilterColumnName),
                    new ClaimModel(AuthConstants.FUserColumname,string.IsNullOrEmpty(intialInfo.FilterUserColumnName) ? "" : intialInfo.FilterUserColumnName)
                };

                var token = TokenBuilder.CreateJsonWebToken(
                            model.UserNameOrEmail,
                            claims,
                            _jwtConfiguration.Audience,
                            _jwtConfiguration.Issuer,
                            Guid.NewGuid(),
                            DateTime.UtcNow.AddMinutes(Convert.ToInt32(_jwtConfiguration.ExpireTime)));

                return new AuthenticateUserOutputDto(!result.IsPasswordChange, token, _jwtConfiguration.ExpireTime);
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
        /// <remarks>\
        /// If the user is valid a token will be sent to the registered email \
        /// The token is valid for 3 hours
        /// </remarks>
        [HttpPost("ForgotPassword")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ForgotPassword(ForgotPassDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var intialInfo = await GetTenantDbInfo(model.HostName);
                if (intialInfo == null || string.IsNullOrEmpty(intialInfo.TenantDBServer))
                {
                    ModelState.AddModelError("", "Invalid Host Name.");
                    return BadRequest(ModelState);
                }

                bool user = await _loginService.AuthenticateUserByEmail(intialInfo.GetConnectionString(), model.EmailAddress);
                if (!user)
                    return StatusCode(StatusCodes.Status404NotFound, "Email address in not valid.");

                var token = await _loginService.GeneratePasswordResetToken(intialInfo.GetConnectionString(), model.EmailAddress);

                _emailNotificationService.SendAsyncEmail(model.EmailAddress, "Password reset token", token, true);
                
                return Ok();

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
        public async Task<ActionResult> ResetPassword(ResetPassDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var intialInfo = await GetTenantDbInfo(model.HostName);
                if (intialInfo == null || string.IsNullOrEmpty(intialInfo.TenantDBServer))
                {
                    ModelState.AddModelError("", "Invalid Host Name.");
                    return BadRequest(ModelState);
                }

                string userResult = await _loginService.ResetUserPassByToken(intialInfo.GetConnectionString(), model.Email, model.Token, model.Password);
                if (!string.IsNullOrEmpty(userResult))
                    return StatusCode(StatusCodes.Status401Unauthorized, userResult);
                else
                    return Ok("Password Reset Successfully.");

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
                var intialInfo = await GetTenantDbInfo(model.HostName);
                if (intialInfo == null || string.IsNullOrEmpty(intialInfo.TenantDBServer))
                {
                    ModelState.AddModelError("", "Invalid Host Name.");
                    return BadRequest(ModelState);
                }

                bool result = await _loginService.ChangeUserPassword(intialInfo.GetConnectionString(), model.Email, model.OldPassword, model.NewPassword);

                if (!result)
                    return StatusCode(StatusCodes.Status401Unauthorized, "Invalid Email & old password.");
                else
                    return Ok("Password Changed Successfully.");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
        
        #region Helpers

        private async Task<CustomerInitialInfoModel> GetTenantDbInfo(string hostName)
        {
            var intialInfo = await _adminService.GetCustomerInialInfo(hostName);

            // TODO: Remove this in live
            intialInfo.TenantDBServer = "74.208.24.39";
            intialInfo.TenantDBName = "PrintingBI-Customer1";
            intialInfo.TenantDBUser = "postgres";
            intialInfo.TenantDBPassword = "Printerbi@.";

            return intialInfo;
        }

        #endregion
    }
}