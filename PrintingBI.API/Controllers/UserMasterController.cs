using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrintingBI.API.Models;
using PrintingBI.Common;
using PrintingBI.Services.UserMaster;

namespace PrintingBI.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize(Roles = RoleModel.SuperAdmin)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class UserMasterController : ControllerBase
    {
        private readonly IUserMasterService _userService;
        private readonly ILogger<UserMasterController> _logger;

        public UserMasterController(IUserMasterService userService, ILogger<UserMasterController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// This api returns the list of Users
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<UserDto>> GetAllUsers()
        {
            return _userService.GetAll<IEnumerable<UserDto>>().ToList();
        }

        /// <summary>
        /// this api returns single user object 
        /// </summary>
        /// <param name="id"> Pass User Id </param>
        /// <returns>Individual user</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDto> GetUser(int id)
        {
            var user = _userService.GetById<UserDto>(id);
            if (user == null)
                return NotFound();

            return user;
        }

        /// <summary>
        /// Create new user with required details
        /// </summary>
        /// <param name="obj">User object with all required information</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> CreateUser(CreateUserDto obj)
        {
            try
            {
                string result = await _userService.InsertUser(obj);
                if (!string.IsNullOrEmpty(result))
                {
                    return StatusCode(StatusCodes.Status409Conflict,result);
                }

                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// This api will update the user object with passed information
        /// </summary>
        /// <param name="id">user id</param>
        /// <param name="obj">user model with updated information</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateUser(int id, UpdateUserDto obj)
        {
            try
            {
                string result = await _userService.UpdateUser(id,obj);
                if (!string.IsNullOrEmpty(result))
                {
                    return StatusCode(StatusCodes.Status409Conflict, result);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// this api will delete specified user object
        /// </summary>
        /// <param name="id">pass user id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}