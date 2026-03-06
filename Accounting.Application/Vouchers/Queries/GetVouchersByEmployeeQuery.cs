using Accounting.Application.Exceptions;
using Accounting.Application.Vouchers.DTOs;
using Accounting.Application.Vouchers.Mappers;
using Accounting.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Vouchers.Queries
{
    public record GetVouchersByEmployeeQuery(int employeeId) : IRequest<List<VoucherResponse>>
    {
        public class GetVouchersByEmployeeQueryHandler : IRequestHandler<GetVouchersByEmployeeQuery, List<VoucherResponse>>
        {
            private readonly IVoucherRepository _voucherRepository;
            public GetVouchersByEmployeeQueryHandler(IVoucherRepository voucherRepository)
            {
                _voucherRepository = voucherRepository;
            }
            public async Task<List<VoucherResponse>> Handle(GetVouchersByEmployeeQuery request, CancellationToken cancellationToken)
            {
                var vouchers = await _voucherRepository.GetVouchersByIdEmployeeAsync(request.employeeId, cancellationToken);
                return vouchers.ToResponseList() ?? new List<VoucherResponse>();
            }
        }   
    }
}
