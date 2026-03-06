using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Entities
{
    public class EmployeeEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime HireDate { get; set; } 
        public decimal Salary { get; set; }
        public string Department { get; set; } = null!;
        public string Position { get; set; } = null!;

        public ICollection<VoucherEntity> Vouchers { get; set; } = new List<VoucherEntity>();
    }
}
