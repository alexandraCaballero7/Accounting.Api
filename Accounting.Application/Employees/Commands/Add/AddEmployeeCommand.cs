using Accounting.Application.Employees.DTOs;
using Accounting.Application.Employees.Mappers;
using Accounting.Core.Entities;
using Accounting.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Employees.Commands.Add
{
 public record AddEmployeeCommand(AddEmployeeRequest Employee) : IRequest<EmployeeResponse>;
    public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, EmployeeResponse>
    {
      private readonly IEmployeeRepository _employeeRepository;
       public AddEmployeeCommandHandler(IEmployeeRepository employeeRepository)
       {
          _employeeRepository = employeeRepository;
       }

        public async Task<EmployeeResponse> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
            {
         
                var entity = new EmployeeEntity
                {
                    FirstName = request.Employee.FirstName,
                    LastName = request.Employee.LastName,
                    Email = request.Employee.Email,
                    Phone = request.Employee.Phone,
                    HireDate = request.Employee.HireDate,
                    Salary = request.Employee.Salary,
                    Department = request.Employee.Department,
                    Position = request.Employee.Position,
                };

                var created = await _employeeRepository.AddEmployeeAsync(entity, cancellationToken);

                return created.ToResponse();
        }
        }
    }

