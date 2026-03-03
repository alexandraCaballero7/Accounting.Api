using Accounting.Core.Entities;
using Accounting.Core.Interfaces;
using Accounting.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Infraestructure.Repositories
{
    public class VoucherRepository(AccountingDbContext dbContext):IVoucherRepository
    {
        public async Task<IEnumerable<VoucherEntity>> GetAllVouchersAsync(CancellationToken cancellationToken)
        {
            return await dbContext.Vouchers
                .Include(x=>x.Items)
                .Include(x=>x.Employee)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<VoucherEntity> GetVoucherByIdVoucherAsync(int VoucherId, CancellationToken cancellationToken)
        {
            return await dbContext.Vouchers
                .Include(x=>x.Items)
                .Include(x=>x.Employee)
                .AsNoTracking()
                .FirstOrDefaultAsync(x=>x.Id == VoucherId, cancellationToken);
        }

        public async Task<IEnumerable<VoucherEntity>> GetVouchersByIdEmployeeAsync(int EmployeeId,CancellationToken cancellationToken)
        {
            return await dbContext.Vouchers
                .Include(x=>x.Items)
                .Include(x => x.Employee)
                .AsNoTracking()
                .Where(x => x.EmployeeId == EmployeeId).ToListAsync(cancellationToken);
        }

        public async Task<VoucherEntity> AddVoucherAsync(VoucherEntity entity, CancellationToken cancellationToken)
        {
            
            dbContext.Vouchers.Add(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<VoucherEntity> UpdateVoucherAsync(int VoucherId, VoucherEntity entity, CancellationToken cancellationToken) 
        {
            var voucher = await dbContext.Vouchers
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == VoucherId, cancellationToken);

            if (voucher == null)
                return null;

            voucher.VoucherNumber = entity.VoucherNumber;
            voucher.Date = entity.Date;
            voucher.Description = entity.Description;
            voucher.EmployeeId = entity.EmployeeId;
            voucher.TotalAmount = entity.TotalAmount;

            if (voucher.Items != null && voucher.Items.Any())
                dbContext.VoucherItems.RemoveRange(voucher.Items);

            if (entity.Items != null && entity.Items.Any())
            {
                foreach (var item in entity.Items)
                    item.VoucherId = voucher.Id;

                await dbContext.VoucherItems.AddRangeAsync(entity.Items, cancellationToken);
                voucher.Items = entity.Items;
            }

            await dbContext.SaveChangesAsync(cancellationToken);

            return voucher; 
        }

        public async Task<bool> DeleteVoucherAsync(int VoucherId, CancellationToken cancellationToken)
        {
            var voucher = await dbContext.Vouchers.FirstOrDefaultAsync(x => x.Id == VoucherId);
           
            if (voucher is null) return false;

            dbContext.Vouchers.Remove(voucher);
            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
            
          
        }
    }
}
