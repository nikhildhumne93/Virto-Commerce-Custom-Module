using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Techment.MyEmployeeModule.Core;
using Techment.MyEmployeeModule.Core.Models;
using VirtoCommerce.Platform.Core.GenericCrud;

namespace Techment.MyEmployeeModule.Web.Controllers.Api
{
    [Route("api/my-employee-module")]
    public class MyEmployeeModuleController : Controller
    {
        private readonly ICrudService<Employee> _employeeService;
        private readonly ISearchService<EmployeeSearchCriteria, EmployeeSearchResult, Employee> _employeeSearchService;
        public MyEmployeeModuleController(ICrudService<Employee> employeeService, ISearchService<EmployeeSearchCriteria, EmployeeSearchResult, Employee> employeeSearchService)
        {
            _employeeSearchService = employeeSearchService;
            _employeeService = employeeService;
        }


        [HttpPost]
        [Route("search")]
        [Authorize(ModuleConstants.Security.Permissions.Read)]
        public async Task<ActionResult<EmployeeSearchResult>> GetAll([FromBody]EmployeeSearchCriteria searchCriteria)
        {
            var result = await _employeeSearchService.SearchAsync(searchCriteria);
            return Ok(result);
        }

        [HttpGet("{employeeById}")]
        public async Task<ActionResult<Employee>> getemployeebyid([FromQuery] string employeeById)
        {
            var criteria = new EmployeeSearchCriteria
            {
                Skip = 0,
                Take = 1,
                ObjectIds = new[] { employeeById }
            };
            var result = await _employeeSearchService.SearchAsync(criteria);
            return Ok(result);
        }

        [HttpPost]
        [Route("")]
        [Authorize(ModuleConstants.Security.Permissions.Create)]
        public async Task<ActionResult<Employee>> Add([FromBody] Employee employee)
        {
            await _employeeService.SaveChangesAsync([employee]);
            return Ok(employee);
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult<Employee>> Update([FromBody] Employee employee)
        {
            await _employeeService.SaveChangesAsync([employee]);
            return Ok(employee);
        }

        [HttpPost]
        [Route("remove")]
        [Authorize(ModuleConstants.Security.Permissions.Delete)]
        public async Task<ActionResult<Employee>> Delete([FromQuery]string ids)
        {
            await _employeeService.DeleteAsync([ids]);

            return Ok();
        }
    }
}
