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
    [Produces("application/json")]
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

            if (user != null && !await _userManager.IsLockedOutAsync(user))
            {
                if(await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError("", "Email is not confirmed.");
                        return BadRequest(ModelState);
                    }

                    await _userManager.ResetAccessFailedCountAsync(user);

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

                await _userManager.AccessFailedAsync(user);
                
                if(await _userManager.IsLockedOutAsync(user))
                {
                    // TODO: Email user of lock out 
                }

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
                var token = _userManager.GenerateEmailConfirmationTokenAsync(user);

                // TODO: Send an email to the user for confirming the email

                return Ok(token);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return BadRequest(ModelState);
            }
        }

        [HttpPost("forgotpassword")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordInputDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return NotFound();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // TODO: Send an email to the user with token

            return Ok(token);
        }

        [HttpPost("resetpassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordInputDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("validation", "Invalid request");
                return BadRequest(ModelState);
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("validation", error.Description);
                }
                return BadRequest(ModelState);
            }
             
            if(await _userManager.IsLockedOutAsync(user))
            {
                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
            }

            return Ok();
        }

        [HttpPost("confirmemail")]
        public async Task<ActionResult> ConfirmEmail(ConfirmEmailInputDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid request");
                return BadRequest(ModelState);
            }

            var result = await _userManager.ConfirmEmailAsync(user, model.Token);

            if (result.Succeeded)
                return Ok();

            ModelState.AddModelError("","Could not confirm the email.");
            return BadRequest(ModelState);
        }
    }
}
