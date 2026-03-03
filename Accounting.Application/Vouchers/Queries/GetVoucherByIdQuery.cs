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
    public record GetVoucherByIdQuery(int VoucherId) : IRequest<VoucherResponse> 
    {
        public class GetVoucherByIdQueryHandler : IRequestHandler<GetVoucherByIdQuery, VoucherResponse>
        {
            private readonly IVoucherRepository _voucherRepository;
            public GetVoucherByIdQueryHandler(IVoucherRepository voucherRepository)
            {
                _voucherRepository = voucherRepository;
            }
            public async Task<VoucherResponse> Handle(GetVoucherByIdQuery request, CancellationToken cancellationToken)
            {
                var voucher = await _voucherRepository.GetVoucherByIdVoucherAsync(request.VoucherId, cancellationToken);
                if (voucher == null)
                    throw new NotFoundException($"Voucher not found");  
                return voucher.ToResponse();
            }
        }
    }
}
