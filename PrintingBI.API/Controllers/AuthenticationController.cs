using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PrintingBI.API.Configuration;
using PrintingBI.API.Models;
using PrintingBI.Authentication;
using PrintingBI.Authentication.Models.Dtos;
using PrintingBI.Services.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrintingBI.API.Controllers
{

    /// <summary>
    /// Authentication handler
    /// </summary>
    [Route("api/auth")]
    [ApiController]
    [Produces("application/json")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<UserMaster> _userManager;
        private readonly IJwtConfiguration _jwtConfiguration;

        public AuthenticationController(IJwtConfiguration jwtConfiguration,
                                        UserManager<UserMaster> userManager)
        {
            _jwtConfiguration = jwtConfiguration;
            _userManager = userManager;
        }

        /// <summary>
        /// Authenticates the user
        /// </summary>
        /// <param name="model">Email and password</param>
        /// <returns></returns>
        /// <remarks>
        /// Password policy : 1 UC, 1 LC, min 6 char, 1 special char \
        /// 3 failed attempt will lockout the user for 10 mins \
        /// Successfull attempt will result in token \
        /// Token has to be passed with everyrequest as Authorization attribute \
        /// in header of the request prefixed with Beared
        /// </remarks>
        [HttpPost("AuthenticateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> AuthenticateUser(AuthenticateUserInputDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                if(await _userManager.IsLockedOutAsync(user))
                {
                    ModelState.AddModelError("validation", "User is locked. Please try after some time");
                    return BadRequest(ModelState);
                }

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
                                _jwtConfiguration.Audience,
                                _jwtConfiguration.Issuer,
                                Guid.NewGuid(),
                                DateTime.UtcNow.AddMinutes(Convert.ToInt32(_jwtConfiguration.ExpireTime)));

                    return Ok(new
                    {
                        token,
                        expires = _jwtConfiguration.ExpireTime
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

        [HttpPost("Register")]
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

        [HttpPost("ResendConfirmationEmail")]
        public async Task<ActionResult> ResendConfirmationEmail(ResendConfirmationEmailInputDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("validation", "Invalid request");
                return BadRequest(ModelState);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // TODO: Send an email to the user with confirm email
            await System.IO.File.WriteAllTextAsync("ConfirmEmailToken.txt",token);

            return Ok();
        }

        [HttpPost("ConfirmEmail")]
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

            ModelState.AddModelError("", "Could not confirm the email.");
            return BadRequest(ModelState);
        }

        [HttpPost("ForgotPassword")]
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

        [HttpPost("ResetPassword")]
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

        [HttpPost("ChangePassword")]
        public async Task<ActionResult> ChangePassword(ChangePasswordInputDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("validation", "Invalid request");
                return BadRequest(ModelState);
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("validation", error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok();
        }

        // TODO: Refresh Token

    }
}
