using Microsoft.AspNetCore.Mvc;
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
    }
}
