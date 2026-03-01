
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Employee → Vouchers (1:N)
            modelBuilder.Entity<EmployeeEntity>()
                .HasMany(e => e.Vouchers)
                .WithOne(v => v.Employee)
                .HasForeignKey(v => v.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict); // Evita borrar empleados con vouchers

            // Voucher → VoucherItems (1:N) con cascada
            modelBuilder.Entity<VoucherEntity>()
                .HasMany(v => v.Items)
                .WithOne(i => i.Voucher)
                .HasForeignKey(i => i.VoucherId)
                .OnDelete(DeleteBehavior.Cascade); // Esto borra los items al borrar el voucher
        }

    }
}



