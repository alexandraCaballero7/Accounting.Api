using System.ComponentModel.DataAnnotations;

namespace Accounting.Application.Employees.Commands.Add
{
    public record AddEmployeeRequest
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 100 characters")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 100 characters")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid phone format")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Hire date is required")]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(0.01, 10000000, ErrorMessage = "Salary must be between 0.01 and 10,000,000")]
        public decimal Salary { get; set; }
    }
}
