using Accounting.Application.Employees.Commands.Add;
using Accounting.Application.Vouchers.Commands.Add;
using Accounting.Application.Vouchers.Commands.Delete;
using Accounting.Application.Vouchers.Commands.Update;
using Accounting.Application.Vouchers.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VouchersController(ISender sender) : ControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> GetAllVouchersAsync( )
        {
            var result = await sender.Send(new GetAllVouchersQuery());
            return Ok(result);
        }

        [HttpGet("{VoucherId}")]
        public async Task<IActionResult> GetVoucherByIdAsync(int VoucherId)
        {
            var result = await sender.Send(new GetVoucherByIdQuery(VoucherId));
            return Ok(result);
        }

        [HttpGet("Employee/{EmployeeId}")]
        public async Task<IActionResult> GetVouchersByEmployeeAsync(int EmployeeId)
        {
            var result = await sender.Send(new GetVouchersByEmployeeQuery(EmployeeId));
            return Ok(result);
        }


        [HttpPost("")]
        public async Task<IActionResult> AddVoucherAsync([FromBody] AddVoucherRequest Voucher)
        {
            var result = await sender.Send(new AddVouchersCommand(Voucher));
            return CreatedAtAction(nameof(GetVoucherByIdAsync), new { VoucherId = result.VoucherId }, result);
        }

        [HttpPut("{VoucherId}")]
        public async Task<IActionResult> UpdateVoucherByIdAsync(int VoucherId, [FromBody] UpdateVoucherRequest voucher)
        {
            var result = await sender.Send(new UpdateVoucherCommand(VoucherId, voucher));
            return Ok(result);
        }

        [HttpDelete("{VoucherId}")]
        public async Task<IActionResult> DeleteVoucherByIdAsync(int VoucherId)
        {
            var result = await sender.Send(new DeleteVoucherCommand(VoucherId));
            return NoContent();
        }
    }
}
