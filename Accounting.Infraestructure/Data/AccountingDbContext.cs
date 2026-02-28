
using Accounting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Infraestructure.Data
{
    public class AccountingDbContext(DbContextOptions<AccountingDbContext> options ): DbContext(options)
    {
        public  DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<VoucherItemEntity> VoucherItems { get; set; } 
        public DbSet<VoucherEntity> Vouchers { get; set; }

    }
}



