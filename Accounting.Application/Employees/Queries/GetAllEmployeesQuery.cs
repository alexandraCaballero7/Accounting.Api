using Accounting.Application.Employees.DTOs;
using Accounting.Application.Employees.Mappers;
using Accounting.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Employees.Queries
{
    public record GetAllEmployeesQuery():IRequest<List<EmployeeResponse>>
    {
        public class GetEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, List<EmployeeResponse>>
        {
            private readonly IEmployeeRepository _employeeRepository;
            public GetEmployeesQueryHandler(IEmployeeRepository employeeRepository)
            {
                _employeeRepository = employeeRepository;
            }
            public async Task<List<EmployeeResponse>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
            {
                var employees = await _employeeRepository.GetAllEmployeesAsync(cancellationToken);

                return employees?.ToResponseList() ?? new List<EmployeeResponse>();
            }
        }   
    }
}
