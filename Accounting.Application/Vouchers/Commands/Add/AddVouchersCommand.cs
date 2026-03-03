using Accounting.Application.Exceptions;
using Accounting.Application.Vouchers.DTOs;
using Accounting.Application.Vouchers.Mappers;
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
        private readonly IEmployeeRepository _employeeRepository;
        public AddVoucherCommandHandler(IVoucherRepository voucherRepository, IEmployeeRepository employeeRepository)
        { 
            _voucherRepository = voucherRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<VoucherResponse> Handle(AddVouchersCommand request, CancellationToken cancellationToken)
        {
            if (request.Voucher == null || request.Voucher.Items == null || !request.Voucher.Items.Any())
                throw new BusinessException("Voucher must have at least one item");

            if (request.Voucher.VoucherNumber == 0)
                throw new BusinessException("VoucherNumber is required.");

            var employeeExists = await _employeeRepository.GetEmployeeByIdAsync(request.Voucher.EmployeeId, cancellationToken);
            if (employeeExists == null)
                throw new NotFoundException("Employee not found");

            var entity = new VoucherEntity
            {
                VoucherNumber = request.Voucher.VoucherNumber,
                Date = request.Voucher.Date, 
                Description = request.Voucher.Description,
                EmployeeId = request.Voucher.EmployeeId,
                TotalAmount = request.Voucher.Items.Sum(x => x.Type == 1 ? -Math.Abs(x.Amount) : x.Amount),
                Items = request.Voucher.Items.Select(x => new VoucherItemEntity
                {
                    Description = x.Description,
                    Amount = x.Type == 1 ? -Math.Abs(x.Amount) : x.Amount,
                    Type = x.Type == 1 ? true : false
                }).ToList()
            };

            var created = await _voucherRepository.AddVoucherAsync(entity,cancellationToken);
           
            if (created == null)
                throw new BusinessException("Error creating voucher");

            return created.ToResponse();
        }
    }
}

