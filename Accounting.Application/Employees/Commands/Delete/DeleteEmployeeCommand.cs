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
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(request.EmployeeId);
            
            if(existingEmployee == null)
            {
                return false; 
            }

            var vouchers = await _voucherRepository.GetVouchersByIdEmployeeAsync(request.EmployeeId);
            if (vouchers != null && vouchers.Any())
            {
                return false;
            }

            return await _employeeRepository.DeleteEmployeeAsync(request.EmployeeId);
        }
    }

}
