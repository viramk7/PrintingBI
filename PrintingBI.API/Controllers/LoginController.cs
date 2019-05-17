using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrintingBI.API.Configuration;
using PrintingBI.API.Helper;
using PrintingBI.API.Models;
using PrintingBI.Authentication;
using PrintingBI.Authentication.Models.Dtos;
using PrintingBI.Services.LoginService;

namespace PrintingBI.API.Controllers
{
    [Route("api/login")]
    [ApiController]
    [Produces("application/json")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IHttpClientHelper<CustomerInitialInfoModel> _httpClientHelper;
        private readonly ILoginService _loginService;
        private readonly IJwtConfiguration _jwtConfiguration;
        private static string validateHostUrl = "api/Settings/GetTenantSetting?hostName=";

        public LoginController(ILogger<LoginController> logger , IHttpClientHelper<CustomerInitialInfoModel> httpClientHelper , ILoginService loginService , IJwtConfiguration jwtConfiguration)
        {
            _logger = logger;
            _httpClientHelper = httpClientHelper;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CustomerInitialInfoModel> AuthenticateLoginUser(LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                string validateUrl = validateHostUrl + model.HostName;
                CustomerInitialInfoModel intialInfo = _httpClientHelper.Get(validateUrl);
                if (intialInfo != null && !string.IsNullOrEmpty(intialInfo.TenantDBServer))
                {
                        //: TODO : Change static DB info , Change staitc Base url from HttpClientHelper

                        intialInfo.TenantDBServer = "74.208.24.39";
                        intialInfo.TenantDBName = "PrintingBI-Customer1";
                        intialInfo.TenantDBUser = "postgres";
                        intialInfo.TenantDBPassword = "Printerbi@.";

                        bool result = _loginService.AuthenticateUser(intialInfo.ConnectionString, model.UserName, model.Password);
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
            catch(Exception ex)
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
        public ActionResult ForgotPassword(ForgotPassDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                string validateUrl = validateHostUrl + model.HostName;
                CustomerInitialInfoModel intialInfo = _httpClientHelper.Get(validateUrl);
                if (intialInfo != null && !string.IsNullOrEmpty(intialInfo.TenantDBServer))
                {
                    intialInfo.TenantDBServer = "74.208.24.39";
                    intialInfo.TenantDBName = "PrintingBI-Customer1";
                    intialInfo.TenantDBUser = "postgres";
                    intialInfo.TenantDBPassword = "Printerbi@.";

                    bool user = _loginService.AuthenticateUserByEmail(intialInfo.ConnectionString , model.EmailAddress);
                    if (!user)
                        return StatusCode(StatusCodes.Status404NotFound, "Email address in not valid.");

                    var token = _loginService.GeneratePasswordResetToken(intialInfo.ConnectionString, model.EmailAddress);
                    return Ok(token);
                }

                // TODO: Send an email to the user with token

                return StatusCode(StatusCodes.Status401Unauthorized,"Request is not Unthorized.");
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult ResetPassword(ResetPassDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                string validateUrl = validateHostUrl + model.HostName;
                CustomerInitialInfoModel intialInfo = _httpClientHelper.Get(validateUrl);
                if (intialInfo != null && !string.IsNullOrEmpty(intialInfo.TenantDBServer))
                {
                    intialInfo.TenantDBServer = "74.208.24.39";
                    intialInfo.TenantDBName = "PrintingBI-Customer1";
                    intialInfo.TenantDBUser = "postgres";
                    intialInfo.TenantDBPassword = "Printerbi@.";

                    bool user = _loginService.ResetUserPassByToken(intialInfo.ConnectionString, model.Token, model.Password);
                    if (!user)
                        return StatusCode(StatusCodes.Status404NotFound, "Invalid request or token is expired.");
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
    }
}