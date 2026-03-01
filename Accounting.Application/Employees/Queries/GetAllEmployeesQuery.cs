using Accounting.Application.Employees.DTOs;
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
                var employees = await _employeeRepository.GetAllEmployeesAsync();
                if (employees == null || !employees.Any())
                {
                    return null;    
                }

                return employees.Select(e => new EmployeeResponse(
                    e.Id,
                    e.FirstName,
                    e.LastName,
                    e.Email,
                    e.Phone,
                    e.HireDate,
                    e.Salary
                )).ToList();
            }
        }   
    }
}
