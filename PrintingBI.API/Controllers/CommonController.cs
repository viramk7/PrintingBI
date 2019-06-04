using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        /// This api returns list of departments with department id and department name.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Used when creating or editing user
        /// </remarks>
        [HttpGet("GetAllDepartments")]
        [HttpGet("GetAllRoleRights")]
        public ActionResult<IEnumerable<DepartmnetDto>> GetAllDepartments()
        {
            return  _commonService.GetAll<IEnumerable<DepartmnetDto>>().ToList();
        }
        
    }
}