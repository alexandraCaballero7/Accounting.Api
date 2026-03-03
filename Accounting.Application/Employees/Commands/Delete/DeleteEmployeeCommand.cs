using Accounting.Application.Exceptions;
using Accounting.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Employees.Commands.Delete
{
    public record DeleteEmployeeCommand(int EmployeeId) : IRequest<bool>;
    
    public class DeleteEmployeeCommandHandler:IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IVoucherRepository _voucherRepository;

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository, IVoucherRepository voucherRepository)
        {
            _employeeRepository = employeeRepository;
            _voucherRepository = voucherRepository;
        }
        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(request.EmployeeId, cancellationToken);

            if (employee == null)
                throw new NotFoundException($"Employee with Id {request.EmployeeId} not found.");

            var vouchers = await _voucherRepository.GetVouchersByIdEmployeeAsync(request.EmployeeId,cancellationToken);
            if (vouchers != null && vouchers.Any())
                throw new BusinessException("Cannot delete employee because they have associated vouchers.");

            return await _employeeRepository.DeleteEmployeeAsync(request.EmployeeId, cancellationToken);
        }
    }

}
