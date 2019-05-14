using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrintingBI.API.Models;
using PrintingBI.Authentication.Configuration;
using PrintingBI.Services.Author;
using PrintingBI.Services.Provisioning;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PrintingBI.API.Controllers
{
    [Authorize]
    [Route("api/author")]
    [ApiController]
    [Produces("application/json")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _service;
        private readonly ICustomerDbInfo _customerDbInfo;
        private readonly IProvisioningService _provisioningService;

        public AuthorController(IAuthorService service, 
                                ICustomerDbInfo customerDbInfo,
                                IProvisioningService provisioningService)
        {
            _service = service;
            _customerDbInfo = customerDbInfo;
            _provisioningService = provisioningService;
        }

        [HttpGet("Provision")]
        public async Task<ActionResult> Provision()
        {
            var (createdAll, errors) = await _provisioningService.Provision();

            if (createdAll)
                return Ok();
            else
                return StatusCode(StatusCodes.Status500InternalServerError, errors);
        }

        /// <summary>
        /// Gets all the authors
        /// </summary>
        /// <returns>List of authors</returns>
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<AuthorsDto>> GetAllAuthors()
        {
            var DbServer = _customerDbInfo.DbServer;
            var dbName = _customerDbInfo.DbName;
            var dbUser = _customerDbInfo.DbUser;
            var dbPwd = _customerDbInfo.DbPwd;

            return _service.GetAll<IEnumerable<AuthorsDto>>().ToList();
        }

        /// <summary>
        /// Gets the author by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Individual author</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AuthorsDto> GetAuthor(int id)
        {
            var author = _service.GetById<AuthorsDto>(id);
            if (author == null)
                return NotFound();

            return author;
        }

        /// <summary>
        /// Create new author
        /// </summary>
        /// <param name="authorCreateDto">fistname and last name</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateAuthor(AuthorCreateDto authorCreateDto)
        {
            _service.Insert(authorCreateDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateAuthor(int id,AuthorUpdateDto authorUpdateDto)
        {
            _service.Update(id,authorUpdateDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAuthor(int id)
        {
            _service.Delete(id);
            return Ok();
        }

    }
}
