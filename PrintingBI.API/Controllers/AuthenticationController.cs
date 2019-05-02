using Microsoft.AspNetCore.Mvc;
using PrintingBI.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrintingBI.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtConfiguration _jwtConfiguration;

        public AuthenticationController(IUserService userService, IJwtConfiguration jwtConfiguration)
        {
            _userService = userService;
            _jwtConfiguration = jwtConfiguration;
        }

        [HttpGet]
        public ActionResult AuthenticateUser(string email, string password)
        {
            var isAuthenticated = _userService.AuthenticateUser(email, password);

            if (!isAuthenticated)
                return BadRequest();

            var token = TokenBuilder.CreateJsonWebToken(
                            email,
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
