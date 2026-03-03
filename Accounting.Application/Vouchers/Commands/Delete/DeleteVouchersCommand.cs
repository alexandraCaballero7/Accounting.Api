using Accounting.Application.Exceptions;
using Accounting.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Vouchers.Commands.Delete
{
    public record DeleteVoucherCommand(int VoucherId) : IRequest<bool>;
    
    public class DeleteVoucherCommandHandler : IRequestHandler<DeleteVoucherCommand, bool>
    {
        private readonly IVoucherRepository _voucherRepository;
        public DeleteVoucherCommandHandler(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }
        public async Task<bool> Handle(DeleteVoucherCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _voucherRepository.DeleteVoucherAsync(request.VoucherId, cancellationToken);

            if (!deleted)
                throw new NotFoundException($"Voucher with Id {request.VoucherId} not found.");

            return true;
        }
    }
}
