using Accounting.Application.Vouchers.Commands.Add;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Vouchers.DTOs
{
    public record VoucherResponse(
         int VoucherId,
         int VoucherNumber,
         DateTime Date,
         string Description,
         int EmployeeId,
         VoucherEmployeeResponse Employee,
         Decimal TotalAmount,
         List<VoucherItemResponse> Items
     );

    public record VoucherItemResponse(
         int VoucherItemId,
         string Description,
         decimal Amount,
         int Type
    );

    public record VoucherEmployeeResponse(
    int EmployeeId,
    string FirstName,
    string LastName
    );

}
