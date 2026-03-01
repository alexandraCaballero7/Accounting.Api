using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Employees.Commands.Update
{
    public class UpdateEmployeRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
    }
}
