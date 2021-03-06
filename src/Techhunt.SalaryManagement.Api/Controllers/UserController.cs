using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Techhunt.SalaryManagement.Application;
using Techhunt.SalaryManagement.Domain;

namespace Techhunt.SalaryManagement.Api.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : Controller
    {
        private EmployeeService _employeeService;

        public UserController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload()
        {
            if (Request.Form.Files.Count != 1)
            {
                return BadRequest("Request contains more than one attachement.");
            }

            try
            {
                var file = Request.Form.Files[0];
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    await _employeeService.Create(stream);
                }
                return Ok();
            }
            catch (InvalidEmployeeDataException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery]decimal minSalary,
            [FromQuery]decimal maxSalary,
            [FromQuery]int offset,
            [FromQuery]int limit,
            [FromQuery][ModelBinder(Name = "sort")]EmployeeSortOptions sort)
        {
            try
            {
                var users = await _employeeService.Get(minSalary, maxSalary, offset, limit, sort);
                return new ObjectResult(users);
            }
            catch (InvalidEmployeeDataException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> Create([FromRoute] string id, [FromBody]Employee employee)
        {
            try
            {
                await _employeeService.Create(employee);
                return Ok();
            }
            catch (InvalidEmployeeDataException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] Employee employee)
        {
            try
            {
                await _employeeService.Update(employee);
                return Ok();
            }
            catch (InvalidEmployeeDataException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Read([FromRoute]string id)
        {
            try
            {
                var user = await _employeeService.Get(id);
                return new ObjectResult(user);
            }
            catch (InvalidEmployeeDataException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                await _employeeService.Delete(id);
                return Ok();
            }
            catch (InvalidEmployeeDataException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
