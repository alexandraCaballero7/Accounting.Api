using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Entities
{
    public class VoucherItemEntity
    {
        public int Id { get; set; }
        public int VoucherId { get; set; }
        public string Description { get; set; } = null!;
        public decimal Amount { get; set; }
        public bool Type { get; set; }
        public VoucherEntity Voucher { get; set; } = null!;
    }
}
