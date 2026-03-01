using Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Interfaces
{
    public interface IVoucherRepository
    {
        Task<IEnumerable<VoucherEntity>> GetVouchers();
        Task<VoucherEntity> GetVoucherByIdVoucherAsync(int VoucherId);
        Task<IEnumerable<VoucherEntity>> GetVouchersByIdEmployeeAsync(int EmployeeId);
        Task<VoucherEntity> AddVoucherAsync(VoucherEntity entity);
        Task<VoucherEntity> UpdateVoucherAsync(int VoucherId, VoucherEntity entity);
        Task<bool> DeleteVoucherAsync(int VoucherId);
    }
}
