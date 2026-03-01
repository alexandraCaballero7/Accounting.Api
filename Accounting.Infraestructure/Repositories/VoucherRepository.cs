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
        public async Task<IEnumerable<VoucherEntity>> GetVouchers()
        {
            return await dbContext.Vouchers.ToListAsync();
        }

        public async Task<VoucherEntity> GetVoucherByIdVoucherAsync(int VoucherId)
        {
            return await dbContext.Vouchers
                .Include(x=>x.Items)
                .Include(x=>x.Employee)
                .FirstOrDefaultAsync(x=>x.Id == VoucherId);
        }

        public async Task<IEnumerable<VoucherEntity>> GetVouchersByIdEmployeeAsync(int EmployeeId)
        {
            return await dbContext.Vouchers
                .Include(x=>x.Items)
                .Include(x => x.Employee)
                .Where(x => x.EmployeeId == EmployeeId).ToListAsync();
        }

        public async Task<VoucherEntity> AddVoucherAsync(VoucherEntity entity)
        {
            
            dbContext.Vouchers.Add(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<VoucherEntity> UpdateVoucherAsync(int VoucherId, VoucherEntity entity) 
        {
            var vaucher = await dbContext.Vouchers.FirstOrDefaultAsync(x=> x.Id == VoucherId);
           
            if (vaucher is not null)
            {
                vaucher.VoucherNumber = entity.VoucherNumber;
                vaucher.Description = entity.Description;
                vaucher.Date = entity.Date;
                vaucher.TotalAmount = entity.TotalAmount;
                await dbContext.SaveChangesAsync();

                var vaucherItems = await dbContext.VoucherItems.Where(x=>x.VoucherId == vaucher.Id).ToListAsync();
                if(vaucherItems.Count() > 0)
                {
                    foreach (var item in vaucherItems)
                    {
                        dbContext.VoucherItems.Remove(item);
                        await dbContext.SaveChangesAsync();
                    }

                    foreach (var item in entity.Items)
                    {
                       
                        item.VoucherId = vaucher.Id;
                        dbContext.VoucherItems.Add(item);
                        await dbContext.SaveChangesAsync();
                    }
                }
                else
                {
                    foreach (var item in entity.Items)
                    {
                       
                        item.VoucherId = vaucher.Id;
                        dbContext.VoucherItems.Add(item);
                        await dbContext.SaveChangesAsync();
                    }
                }

                return vaucher;

            }
            return entity;
        }

        public async Task<bool> DeleteVoucherAsync(int VoucherId)
        {
            var voucher = await dbContext.Vouchers.FirstOrDefaultAsync(x => x.Id == VoucherId);
            if (voucher is not null)
            {
                dbContext.Vouchers.Remove(voucher);
                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
