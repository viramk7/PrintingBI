using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrintingBI.API.Configuration;
using PrintingBI.API.Models;
using PrintingBI.Common;
using PrintingBI.Common.Models;
using PrintingBI.Data.CustomModel;
using PrintingBI.Services.AdminTenantService;
using PrintingBI.Services.LoginService;

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

        public LoginController(ILogger<LoginController> logger, 
                              IAdminTenantService adminService, 
                              ILoginService loginService, 
                              IJwtConfiguration jwtConfiguration)
        {
            _logger = logger;
            _adminService = adminService;
            _loginService = loginService;
            _jwtConfiguration = jwtConfiguration;
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

                var result = await _loginService.AuthenticateUser(intialInfo.GetConnectionString(), model.UserNameOrEmail, model.Password, _jwtConfiguration.RefreshTokenExpiryTime);
                if (!result.IsAuthenticated)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Invalid UserName and Password.");
                }

                List<ClaimModel> claims = SetClaims(intialInfo, result);

                var token = TokenBuilder.CreateJsonWebToken(
                            model.UserNameOrEmail,
                            claims,
                            _jwtConfiguration.Audience,
                            _jwtConfiguration.Issuer,
                            Guid.NewGuid(),
                            DateTime.UtcNow.AddMinutes(Convert.ToInt32(_jwtConfiguration.ExpireTime)));

                return new AuthenticateUserOutputDto(!result.IsPasswordChange, token, _jwtConfiguration.ExpireTime,result.RefreshToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
        
        /// <summary>
        /// When the user forgets his/her password and wants to reset using email. 
        /// </summary>
        /// <param name="model">Model contains HostName and email address</param>
        /// <returns></returns>
        /// <remarks>
        /// If it is valid email then it will send an email with link to reset password
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

                _loginService.SendForgotPasswordEmail(token, model.EmailAddress);

                //_emailNotificationService.SendAsyncEmail(model.EmailAddress, "Password reset token", token, true);

                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Api to reset the password after filling the reset password form with token provided in email.
        /// </summary>
        /// <param name="model">Model contains HostName , Token , Email address and new password.</param>
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
        /// Api to change the user password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("ChangePassword")]
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
                    return StatusCode(StatusCodes.Status401Unauthorized, "Invalid Email or old password.");
                else
                    return Ok("Password Changed Successfully.");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Api used when the jwt token is expired. New token will be assigned for seamless user experience.
        /// </summary>
        /// <param name="model">Provide the hostname, username or email and refresh token</param>
        /// <returns></returns>
        [HttpPost("RefreshToken")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AuthenticateUserOutputDto>> ValidateRefreshToken(RefreshTokenDto model)
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

                var result = await _loginService.ValidateRefreshToken(intialInfo.GetConnectionString(), model.UserNameOrEmail, model.RefreshToken, _jwtConfiguration.RefreshTokenExpiryTime);
                if (!result.IsAuthenticated)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Refresh token is not valid or it is expired.");
                }

                List<ClaimModel> claims = SetClaims(intialInfo, result);

                var token = TokenBuilder.CreateJsonWebToken(
                            model.UserNameOrEmail,
                            claims,
                            _jwtConfiguration.Audience,
                            _jwtConfiguration.Issuer,
                            Guid.NewGuid(),
                            DateTime.UtcNow.AddMinutes(Convert.ToInt32(_jwtConfiguration.ExpireTime)));

                return new AuthenticateUserOutputDto(!result.IsPasswordChange, token, _jwtConfiguration.ExpireTime, result.RefreshToken);
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

        private static List<ClaimModel> SetClaims(CustomerInitialInfoModel intialInfo, AuthenticateUserResultDto result)
        {
            return new List<ClaimModel>
                {
                    new ClaimModel(AuthConstants.DbServer,intialInfo.TenantDBServer),
                    new ClaimModel(AuthConstants.DbName,intialInfo.TenantDBName),
                    new ClaimModel(AuthConstants.DbUser,intialInfo.TenantDBUser),
                    new ClaimModel(AuthConstants.DbPwd,intialInfo.TenantDBPassword),
                    new ClaimModel(AuthConstants.PBAppId,intialInfo.ApplicationId),
                    new ClaimModel(AuthConstants.PBUserName,intialInfo.PowerBIUserName),
                    new ClaimModel(AuthConstants.PBPass,intialInfo.PowerBIUserPass),
                    new ClaimModel(AuthConstants.WorkspaceID,intialInfo.WorkSpaceId),
                    new ClaimModel(AuthConstants.IsSuperAdmin,result.IsSuperAdmin.ToString()),
                    new ClaimModel(AuthConstants.FTabName,string.IsNullOrEmpty(intialInfo.FilterTableName) ? "" : intialInfo.FilterTableName),
                    new ClaimModel(AuthConstants.FColumnName,string.IsNullOrEmpty(intialInfo.FilterColumnName) ? "" : intialInfo.FilterColumnName),
                    new ClaimModel(AuthConstants.FUserColumname,string.IsNullOrEmpty(intialInfo.FilterUserColumnName) ? "" : intialInfo.FilterUserColumnName)
                };
        }

        #endregion
    }
}