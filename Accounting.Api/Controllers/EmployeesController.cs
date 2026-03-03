
using Accounting.Application.Employees.Commands.Add;
using Accounting.Application.Employees.Commands.Delete;
using Accounting.Application.Employees.Commands.Update;
using Accounting.Application.Employees.Queries;
using Accounting.Application.Employees.DTOs;
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
        [ProducesResponseType(typeof(List<EmployeeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllEmployeeAsync()
        {
            var result = await sender.Send(new GetAllEmployeesQuery());
            return Ok(result);
        }

        [HttpGet("{employeeId}")]
        [ProducesResponseType(typeof(EmployeeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllEmployeeByIdAsync(int employeeId)
        {
            var result = await sender.Send(new GetEmployeeByIdQuery(employeeId));
            return Ok(result);
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(EmployeeResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] AddEmployeeRequest employee)
        {
            var result = await sender.Send(new AddEmployeeCommand(employee));
            return CreatedAtAction(nameof(GetAllEmployeeByIdAsync), new { employeeId = result.EmployeeId }, result);
        }

        [HttpPut("{employeeId}")]
        [ProducesResponseType(typeof(EmployeeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEmployeeAsync(int employeeId, [FromBody] UpdateEmployeRequest employee)
        {
            var result = await sender.Send(new UpdateEmployeeCommand(employeeId, employee));
            return Ok(result);
        }

        [HttpDelete("{employeeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEmployeeAsync(int employeeId)
        {
            await sender.Send(new DeleteEmployeeCommand(employeeId));
            return NoContent();
        }
    }
}
