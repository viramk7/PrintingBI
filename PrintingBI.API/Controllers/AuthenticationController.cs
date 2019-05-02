using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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

        private readonly string _JwtKey;
        private readonly string _JwtAudience;
        private readonly string _JwtIssuer;
        private readonly string _JwtExpireTime ;

        public AuthenticationController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;

            _JwtKey = configuration["Tokens:key"];
            _JwtAudience = configuration["Tokens:audience"];
            _JwtIssuer = configuration["Tokens:issuer"];
            _JwtExpireTime = configuration["Tokens:expiretime"];

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
                            _JwtAudience,
                            _JwtIssuer,
                            Guid.NewGuid(),
                            DateTime.UtcNow.AddMinutes(Convert.ToInt32(_JwtExpireTime)));

            return Ok(new
            {
                token,
                expires = _JwtExpireTime
            });

        }
    }
}
