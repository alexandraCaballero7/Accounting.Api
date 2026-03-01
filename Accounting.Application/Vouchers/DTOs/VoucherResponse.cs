using Accounting.Application.Vouchers.Commands.Add;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Vouchers.DTOs
{
    public record VoucherResponse(
        int Id,
        int VoucherNumber,
        DateTime Date,
        string Description,
        int EmployeeId,
        Decimal TotalAmount,
        List<VoucherItemResponse> Items
    );

    public record VoucherItemResponse(
         int VoucherId,
         string Description,
         decimal Amount,
         int Type
    );
}
