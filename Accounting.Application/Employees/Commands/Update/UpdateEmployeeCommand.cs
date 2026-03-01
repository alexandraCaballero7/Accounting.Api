using Accounting.Application.Employees.DTOs;
using Accounting.Core.Entities;
using Accounting.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Employees.Commands.Update
{
    public record UpdateEmployeeCommand (int EmployeeId, UpdateEmployeRequest Employee) : IRequest<EmployeeResponse>;
     public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        
        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<EmployeeResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(request.EmployeeId);

            if (existingEmployee is null)
                return null;

            var entity = new EmployeeEntity
            {
                Id = request.Employee.Id,
                FirstName = request.Employee.FirstName,
                LastName = request.Employee.LastName,
                Email = request.Employee.Email,
                Phone = request.Employee.Phone,
                HireDate = request.Employee.HireDate,
                Salary = request.Employee.Salary
            };
            var updated = await _employeeRepository.UpdateEmployeAsync(request.EmployeeId, entity);
            return new EmployeeResponse(
                updated.Id,
                updated.FirstName,
                updated.LastName,
                updated.Email,
                updated.Phone,
                updated.HireDate,
                updated.Salary
            );
        }
    }
}
