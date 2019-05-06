using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrintingBI.API.Models;
using PrintingBI.Authentication;
using PrintingBI.Authentication.Models.Dtos;
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
        private readonly UserManager<UserMaster> _userManager;
        private readonly IJwtConfiguration _jwtConfiguration;

        public AuthenticationController(IUserService userService,
                                        IJwtConfiguration jwtConfiguration,
                                        UserManager<UserMaster> userManager)
        {
            _jwtConfiguration = jwtConfiguration;
            _userManager = userManager;
        }

        [HttpPost("authenticateuser")]
        public async Task<ActionResult> AuthenticateUser(AuthenticateUserInputDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
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

            ModelState.AddModelError("", "Invalid username or password.");
            return BadRequest(ModelState);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterInputDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                ModelState.AddModelError("UserExists", "This email address is already registered with us.");
                return BadRequest(ModelState);
            }

            user = new UserMaster
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return BadRequest(ModelState);
            }
            
            //if (result.Succeeded)
            //{
            //    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //    var confirmationEmail = Url.Action("ConfirmEmailAddress", "Home",
            //        new { token = token, email = user.Email }, Request.Scheme);
            //    System.IO.File.WriteAllText("confirmationLink.txt", confirmationEmail);
            //}
            
        }
    }
}
