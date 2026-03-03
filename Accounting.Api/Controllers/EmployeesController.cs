
using Accounting.Application.Employees.Commands.Add;
using Accounting.Application.Employees.Commands.Delete;
using Accounting.Application.Employees.Commands.Update;
using Accounting.Application.Employees.Queries;
using Accounting.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController(ISender sender) : ControllerBase
    {


        [HttpGet("")]
        public async Task<IActionResult> GetAllEmployeeAsync()
        {
            var result = await sender.Send(new GetAllEmployeesQuery());
            return Ok(result);
        }


        [HttpGet("{EmployeeId}")]
        public async Task<IActionResult> GetAllEmployeeByIdAsync(int EmployeeId)
        {
            var result = await sender.Send(new GetEmployeeByIdQuery(EmployeeId));
            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] AddEmployeeRequest Employee)
        {
            var result = await sender.Send(new AddEmployeeCommand(Employee));
            return CreatedAtAction(nameof(GetAllEmployeeByIdAsync), new { EmployeeId = result.EmployeeId }, result);
        }

        [HttpPut("{EmployeeId}")]
        public async Task<IActionResult> UpdateEmployeeAsync(int EmployeeId, [FromBody] UpdateEmployeRequest employee)
        {
            var result = await sender.Send(new UpdateEmployeeCommand(EmployeeId,employee));
            return Ok(result);
        }

        [HttpDelete("{EmployeeId}")]
        public async Task<IActionResult> DeleteEmployeeAsync(int EmployeeId)
        {
            var result = await sender.Send(new DeleteEmployeeCommand(EmployeeId));
            return NoContent();
        }
    }
}
