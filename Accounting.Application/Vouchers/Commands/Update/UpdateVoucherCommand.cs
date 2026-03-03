using Accounting.Application.Exceptions;
using Accounting.Application.Vouchers.DTOs;
using Accounting.Application.Vouchers.Mappers;
using Accounting.Core.Entities;
using Accounting.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Vouchers.Commands.Update
{
    public record UpdateVoucherCommand(int VoucherId,UpdateVoucherRequest Voucher): IRequest<VoucherResponse>;
     public class UpdateVoucherCommandHandler : IRequestHandler<UpdateVoucherCommand, VoucherResponse>
     {
        private readonly IVoucherRepository _voucherRepository;

        public UpdateVoucherCommandHandler(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public async Task<VoucherResponse> Handle(UpdateVoucherCommand request, CancellationToken cancellationToken)
        {
               if (request.Voucher.Items == null || !request.Voucher.Items.Any())
                throw new BusinessException("Voucher must have at least one item");


            var entity = new VoucherEntity
            {
                Id = request.VoucherId,
                VoucherNumber = request.Voucher.VoucherNumber,
                TotalAmount = request.Voucher.Items.Sum(x => x.Type == 1 ? -Math.Abs(x.Amount) : x.Amount),
                Date = request.Voucher.Date,
                Description = request.Voucher.Description,
                EmployeeId = request.Voucher.EmployeeId,
                Items = request.Voucher.Items.Select(x => new VoucherItemEntity
                {
                    Description = x.Description,
                    Amount = x.Type == 1 ? -Math.Abs(x.Amount) : x.Amount,
                    Type = x.Type == 1 ? true : false
                }).ToList()
            };

            var updated = await _voucherRepository.UpdateVoucherAsync(request.VoucherId, entity, cancellationToken);

            if (updated == null)
                throw new NotFoundException($"Voucher with id {request.VoucherId} not found");


            return updated.ToResponse();
      
        }
    }
}
