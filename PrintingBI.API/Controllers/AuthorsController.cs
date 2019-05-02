using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintingBI.API.Models;
using PrintingBI.Services.Author;
using System.Collections.Generic;
using System.Linq;

namespace PrintingBI.API.Controllers
{
    [Authorize]
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _service;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AuthorsDto>> Get()
        {
            var authors = _service.GetAll();
            return _mapper.Map<IEnumerable<AuthorsDto>>(authors).ToList();
        }
    }
}
