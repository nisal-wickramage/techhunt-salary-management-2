using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Techhunt.SalaryManagement.Domain;

namespace Techhunt.SalaryManagement.Api.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : Controller
    {
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery]decimal minSalary,
            [FromQuery]decimal maxSalary,
            [FromQuery]long offset,
            [FromQuery]int limit,
            [FromQuery]string sort)
        {
            return Ok();
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> Create([FromRoute] string id, [FromBody]Employee employee)
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] Employee employee)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Read([FromRoute]string id)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            throw new NotImplementedException();
        }
    }
}
