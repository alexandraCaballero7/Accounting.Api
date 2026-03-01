namespace Accounting.Application.Employees.Commands.Add
{
    public record AddEmployeeRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
    }
}
