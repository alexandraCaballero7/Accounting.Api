using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Employees.DTOs
{
    public record EmployeeResponse(
        int EmployeeId,
        string FirstName,
        string LastName,
        string Email,
        string Phone,
        DateTime HireDate,
        decimal Salary,
        string Department,
        string Position
    );
}

