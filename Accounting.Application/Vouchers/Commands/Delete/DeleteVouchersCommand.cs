using Accounting.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Vouchers.Commands.Delete
{
    public record DeleteVouchersCommand(int VoucherId) : IRequest<bool>;
    
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteVouchersCommand, bool>
    {
        private readonly IVoucherRepository _voucherRepository;
        public DeleteEmployeeCommandHandler(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }
        public async Task<bool> Handle(DeleteVouchersCommand request, CancellationToken cancellationToken)
        {
            var existingVoucher = await _voucherRepository.GetVoucherByIdVoucherAsync(request.VoucherId);
            
            if(existingVoucher == null)
            {
                return false; 
            }
            return await _voucherRepository.DeleteVoucherAsync(request.VoucherId);
        }
    }
}
