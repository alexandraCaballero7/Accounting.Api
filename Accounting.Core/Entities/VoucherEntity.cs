using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Entities
{
    public class VoucherEntity
    {
        public int Id { get; set; }
        public int VoucherNumber { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public int EmployeeId { get; set; }
        public decimal TotalAmount { get; set; }
        public EmployeeEntity Employee { get; set; } = null!;
        public ICollection<VoucherItemEntity> Items { get; set; } = new List<VoucherItemEntity>();
    }
}
