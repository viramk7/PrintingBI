using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrintingBI.API.Models;
using PrintingBI.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrintingBI.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtConfiguration _jwtConfiguration;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IUserService userService, 
                                        IJwtConfiguration jwtConfiguration,
                                        ILogger<AuthenticationController> logger)
        {
            _userService = userService;
            _jwtConfiguration = jwtConfiguration;
            _logger = logger;
        }

        [HttpPost("authenticateuser")]
        public ActionResult AuthenticateUser([FromForm]AuthenticateUserInputDto model)
        {
            _logger.LogError("First log");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isAuthenticated = _userService.AuthenticateUser(model.Email, model.Password);

            if (!isAuthenticated)
                return BadRequest();

            var token = TokenBuilder.CreateJsonWebToken(
                            model.Email,
                            new List<string>() { "Administrator" },
                            _jwtConfiguration.JwtAudience,
                            _jwtConfiguration.JwtIssuer,
                            Guid.NewGuid(),
                            DateTime.UtcNow.AddMinutes(Convert.ToInt32(_jwtConfiguration.JwtExpireTime)));

            return Ok(new
            {
                token,
                expires = _jwtConfiguration.JwtExpireTime
            });

        }
    }
}
