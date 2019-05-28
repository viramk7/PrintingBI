using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrintingBI.API.Models;
using PrintingBI.Data.Entities;
using PrintingBI.Services.UserMaster;

namespace PrintingBI.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    [Produces("application/json")]
    [Consumes("application/json")]
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
        /// Gets all the users
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<UserDto>> GetAllUsers()
        {
            return _userService.GetAll<IEnumerable<UserDto>>().ToList();
        }

        /// <summary>
        /// Gets the user by id
        /// </summary>
        /// <param name="id"></param>
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
        /// Create new user
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateUser(UserDto obj)
        {
            _userService.Insert(obj);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, UserDto obj)
        {
            _userService.Update(id, obj);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            _userService.DeleteById(id);
            return Ok();
        }
    }
}