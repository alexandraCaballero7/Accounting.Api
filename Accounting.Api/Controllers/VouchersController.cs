using Accounting.Application.Vouchers.Commands.Add;
using Accounting.Application.Vouchers.Commands.Delete;
using Accounting.Application.Vouchers.Commands.Update;
using Accounting.Application.Vouchers.Queries;
using Accounting.Application.Vouchers.DTOs;
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
        [ProducesResponseType(typeof(List<VoucherResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllVouchersAsync()
        {
            var result = await sender.Send(new GetAllVouchersQuery());
            return Ok(result);
        }

        [HttpGet("{voucherId}")]
        [ProducesResponseType(typeof(VoucherResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetVoucherByIdAsync(int voucherId)
        {
            var result = await sender.Send(new GetVoucherByIdQuery(voucherId));
            return Ok(result);
        }

        [HttpGet("Employee/{employeeId}")]
        [ProducesResponseType(typeof(List<VoucherResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetVouchersByEmployeeAsync(int employeeId)
        {
            var result = await sender.Send(new GetVouchersByEmployeeQuery(employeeId));
            return Ok(result);
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(VoucherResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddVoucherAsync([FromBody] AddVoucherRequest voucher)
        {
            var result = await sender.Send(new AddVouchersCommand(voucher));
            return CreatedAtAction(nameof(GetVoucherByIdAsync), new { voucherId = result.VoucherId }, result);
        }

        [HttpPut("{voucherId}")]
        [ProducesResponseType(typeof(VoucherResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateVoucherByIdAsync(int voucherId, [FromBody] UpdateVoucherRequest voucher)
        {
            var result = await sender.Send(new UpdateVoucherCommand(voucherId, voucher));
            return Ok(result);
        }

        [HttpDelete("{voucherId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteVoucherByIdAsync(int voucherId)
        {
            await sender.Send(new DeleteVoucherCommand(voucherId));
            return NoContent();
        }
    }
}
