using Accounting.Application.Employees.Commands.Add;
using Accounting.Application.Vouchers.Commands.Add;
using Accounting.Application.Vouchers.Commands.Delete;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VouchersController(ISender sender) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> AddVoucherAsync([FromBody] AddVoucherRequest voucher)
        {
            var result = await sender.Send(new AddVouchersCommand(voucher));
            return Ok(result);
        }

        [HttpDelete("{VoucherId}")]
        public async Task<IActionResult> DeleteVoucherByIdAsync(int VoucherId)
        {
            var result = await sender.Send(new DeleteVouchersCommand(VoucherId));
            return Ok(result);
        }
    }
}
