using Microsoft.AspNetCore.Mvc;
using PrintingBI.API.Models;
using PrintingBI.Services.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrintingBI.API.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _service;

        public AuthorsController(IAuthorService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AuthorsDto>> Get()
        {
            var authors = _service.GetAll();
            return authors.Select(s => new AuthorsDto(s.Id, s.Name)).ToList();
        }
    }
}
