using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Application.Vouchers.Commands.Update
{
    public class UpdateVoucherRequest
    {
      
            public int VoucherNumber { get; set; }
            public DateTime Date { get; set; }
            public string? Description { get; set; }
            public int EmployeeId { get; set; }

            public ICollection<AddVoucherItemRequest> Items { get; set; }
    }
     public class AddVoucherItemRequest
     {
            public string Description { get; set; } = null!;
            public decimal Amount { get; set; }
            public int Type { get; set; }
     }
}



