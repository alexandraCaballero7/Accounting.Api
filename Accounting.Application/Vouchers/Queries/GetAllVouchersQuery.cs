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
    public record GetAllVouchersQuery() : IRequest<List<VoucherResponse>>
    {
        public class GetAllVouchersQueryHandler : IRequestHandler<GetAllVouchersQuery, List<VoucherResponse>>
        {
            private readonly IVoucherRepository _voucherRepository;
            public GetAllVouchersQueryHandler(IVoucherRepository voucherRepository)
            {
                _voucherRepository = voucherRepository;
            }
            public async Task<List<VoucherResponse>> Handle(GetAllVouchersQuery request, CancellationToken cancellationToken)
            {
                var vouchers = await _voucherRepository.GetAllVouchersAsync(cancellationToken);
                
                return vouchers.ToResponseList() ?? new List<VoucherResponse>();

            }
        }   
    }
}
