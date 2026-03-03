using Accounting.Application.Vouchers.DTOs;
using Accounting.Application.Vouchers.Queries;
using Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Vouchers.Mappers
{
    public static class VoucherResponseMapper
    {
        public static VoucherResponse ToResponse(this VoucherEntity voucher)
        {
            if (voucher == null) return null;

            return new VoucherResponse(
                VoucherId: voucher.Id,
                VoucherNumber: voucher.VoucherNumber,
                Date: voucher.Date,
                Description: voucher.Description ?? string.Empty,
                EmployeeId: voucher.EmployeeId,
                Employee: voucher.Employee != null
                    ? new VoucherEmployeeResponse(
                        EmployeeId: voucher.Employee.Id,
                        FirstName: voucher.Employee.FirstName,
                        LastName: voucher.Employee.LastName
                    )
                    : null,
                TotalAmount: voucher.TotalAmount,
                Items: voucher.Items?.Select(i => new VoucherItemResponse(
                    VoucherItemId: i.Id,
                    Description: i.Description,
                    Amount: i.Amount,
                    Type: i.Type ? 1 : 0
                )).ToList() ?? new List<VoucherItemResponse>()
            );


        }
        public static List<VoucherResponse> ToResponseList(this IEnumerable<VoucherEntity> vouchers)
        {
            return vouchers?.Select(v => v.ToResponse()).ToList() ?? new List<VoucherResponse>();
        }
    }
}
