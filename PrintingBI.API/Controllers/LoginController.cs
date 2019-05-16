using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        public LoginController(ILogger<LoginController> logger , IHttpClientHelper<CustomerInitialInfoModel> httpClientHelper , ILoginService loginService)
        {
            _logger = logger;
            _httpClientHelper = httpClientHelper;
            _loginService = loginService;
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
                
                string validateHostUrl = "api/Settings/GetTenantSetting?hostName=" + model.HostName;
                CustomerInitialInfoModel intialInfo = _httpClientHelper.Get(validateHostUrl);
                if (intialInfo != null && !string.IsNullOrEmpty(intialInfo.TenantDBServer))
                {
                        //: TODO : Change static DB info , Change staitc Base url from HttpClientHelper

                        intialInfo.TenantDBServer = "74.208.24.39";
                        intialInfo.TenantDBName = "PrintingBI-customer1";
                        intialInfo.TenantDBUser = "postgres";
                        intialInfo.TenantDBPassword = "Printerbi@.";

                        bool result = _loginService.AuthenticateUser(intialInfo.ConnectionString, model.UserName, model.Password);
                        if (result)
                        {
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

        //public ActionResult ForgotPassword(ForgotPasswordInputDto model)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    //var user = await _userManager.FindByEmailAsync(model.Email);
        //    //if (user == null)
        //    //    return NotFound();

        //    //var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        //    // TODO: Send an email to the user with token

        //    return Ok();
        //}
    }
}