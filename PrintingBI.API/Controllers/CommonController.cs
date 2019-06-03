using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrintingBI.API.Models;
using PrintingBI.Services.Common;

namespace PrintingBI.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/common")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService _commonService;
        
        public CommonController(ICommonService commonService)
        {
            _commonService = commonService;
        }

        /// <summary>
        /// This api returns list of departments with Id and Name
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllDepartments")]
        public IEnumerable<DepartmnetDto> GetAllDepartments()
        {
            return  _commonService.GetAll<IEnumerable<DepartmnetDto>>();
        }

        /// <summary>
        /// This api returns list of role-rights with Id and Name
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllRoleRights")]
        public IEnumerable<DepartmnetDto> GetAllRoleRights()
        {
            return _commonService.GetAll<IEnumerable<DepartmnetDto>>();
        }
    }
}