using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrintingBI.API.Models;
using PrintingBI.Authentication.Configuration;
using PrintingBI.Data.Entities;
using PrintingBI.Services.Common;

namespace PrintingBI.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/common")]
    [Produces("application/json")]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService _commonService;
        private readonly ILogger<CommonController> _logger;

        public CommonController(ICommonService commonService, ILogger<CommonController> logger)
        {
            _commonService = commonService;
            _logger = logger;
        }

        [HttpGet("GetAllDepartments")]
        public IEnumerable<DepartmnetDto> GetAllDepartments()
        {
            return  _commonService.GetAll<IEnumerable<DepartmnetDto>>();
        }

        [HttpGet("GetAllRoleRights")]
        public IEnumerable<DepartmnetDto> GetAllRoleRights()
        {
            return _commonService.GetAll<IEnumerable<DepartmnetDto>>();
        }
    }
}