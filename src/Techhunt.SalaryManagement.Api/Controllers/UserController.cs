using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
            decimal minSalary, 
            decimal maxSalary, 
            long offset, 
            long limit, 
            string sort)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> Post([FromRoute] string id)
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute]string id)
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
