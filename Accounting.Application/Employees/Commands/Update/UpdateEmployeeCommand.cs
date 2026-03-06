using Accounting.Application.Employees.DTOs;
using Accounting.Application.Employees.Mappers;
using Accounting.Application.Exceptions;
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
            var entity = new EmployeeEntity
            {
                Id = request.EmployeeId,
                FirstName = request.Employee.FirstName,
                LastName = request.Employee.LastName,
                Email = request.Employee.Email,
                Phone = request.Employee.Phone,
                HireDate = request.Employee.HireDate,
                Salary = request.Employee.Salary,
                Department = request.Employee.Department,
                Position = request.Employee.Position,
            };
            var updated = await _employeeRepository.UpdateEmployeAsync(request.EmployeeId, entity, cancellationToken);
            if (updated is null)
                throw new NotFoundException($"Employee with Id {request.EmployeeId} not found.");
            return updated.ToResponse();
        }
    }
}
