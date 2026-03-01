using Accounting.Application.Vouchers.DTOs;
using Accounting.Core.Entities;
using Accounting.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Vouchers.Commands.Add
{
public record AddVouchersCommand(AddVoucherRequest Voucher) : IRequest<VoucherResponse>;
    
    public class AddVoucherCommandHandler : IRequestHandler<AddVouchersCommand, VoucherResponse>
    {
        private readonly IVoucherRepository _voucherRepository;
        public AddVoucherCommandHandler(IVoucherRepository voucherRepository)
        { 
            _voucherRepository = voucherRepository; 
        }

        public async Task<VoucherResponse> Handle(AddVouchersCommand request, CancellationToken cancellationToken)
        {
           
            var entity = new VoucherEntity
            {
                VoucherNumber = request.Voucher.VoucherNumber,
                Date = request.Voucher.Date,
                Description = request.Voucher.Description,
                EmployeeId = request.Voucher.EmployeeId,
                TotalAmount = request.Voucher.Items.Sum(i => i.Amount),
                Items = request.Voucher.Items.Select(i => new VoucherItemEntity
                {
                    Description = i.Description,
                    Amount = i.Amount,
                    Type = i.Type == 1 ? true : false
                }).ToList()
            };

            var created = await _voucherRepository.AddVoucherAsync(entity);

            return new VoucherResponse(
                Id: created.Id,
                VoucherNumber: created.VoucherNumber,
                Date: created.Date,
                Description: created.Description,
                EmployeeId: created.EmployeeId,
                TotalAmount: created.TotalAmount,
                Items: created.Items.Select(i => new VoucherItemResponse(
                    i.VoucherId,
                    i.Description,
                    i.Amount,
                    i.Type == true ? 1 : 0
                )).ToList()
            );
        }
    }
}

