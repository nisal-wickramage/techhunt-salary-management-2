﻿using Microsoft.AspNetCore.Mvc;
using System;
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
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery]decimal minSalary,
            [FromQuery]decimal maxSalary,
            [FromQuery]int offset,
            [FromQuery]int limit,
            [FromQuery]EmployeeSortOptions sort)
        {
            var users = _employeeService.Get(minSalary, maxSalary, offset, limit, sort);
            return new ObjectResult(users);
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> Create([FromRoute] string id, [FromBody]Employee employee)
        {
            await _employeeService.Create(employee);
            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] Employee employee)
        {
            await _employeeService.Update(employee);
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Read([FromRoute]string id)
        {
            var user = await _employeeService.Get(id);
            return new ObjectResult(user);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await _employeeService.Delete(id);
            return Ok();
        }
    }
}
