using Accounting.Application.Employees.DTOs;
using Accounting.Application.Employees.Mappers;
using Accounting.Application.Exceptions;
using Accounting.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Employees.Queries
{
    public record GetEmployeeByIdQuery(int EmployeeId) : IRequest<EmployeeResponse>
    {
        public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeResponse>
        {
            private readonly IEmployeeRepository _employeeRepository;
            public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository)
            {
                _employeeRepository = employeeRepository;
            }
            public async Task<EmployeeResponse> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(request.EmployeeId, cancellationToken);
                if (employee == null)
                   throw new NotFoundException($"Employee with Id {request.EmployeeId} not found.");
                
                return employee.ToResponse();   
            }
        }
    }
}
