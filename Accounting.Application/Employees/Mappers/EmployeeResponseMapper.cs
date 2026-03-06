using Accounting.Application.Employees.DTOs;
using Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Employees.Mappers
{
    public static class EmployeeResponseMapper
    {
        public static EmployeeResponse ToResponse(this EmployeeEntity employee)
        {
            if (employee == null) return null;
            return new EmployeeResponse(
                employee.Id,
                employee.FirstName,
                employee.LastName,
                employee.Email,
                employee.Phone,
                employee.HireDate,
                employee.Salary,
                employee.Department,
                employee.Position
            );
        }

        public static List<EmployeeResponse> ToResponseList(this IEnumerable<EmployeeEntity> employees)
        {
          
            var responseList = new List<EmployeeResponse>();
            foreach (var employee in employees)
            {
                responseList.Add(employee.ToResponse());
            }
            return responseList;
        }
    }
}
