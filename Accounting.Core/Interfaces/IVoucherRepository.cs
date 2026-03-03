using Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Interfaces
{
    public interface IVoucherRepository
    {
        Task<IEnumerable<VoucherEntity>> GetAllVouchersAsync(CancellationToken cancellationToken);
        Task<VoucherEntity> GetVoucherByIdVoucherAsync(int VoucherId, CancellationToken cancellationToken);
        Task<IEnumerable<VoucherEntity>> GetVouchersByIdEmployeeAsync(int EmployeeId, CancellationToken cancellationToken);
        Task<VoucherEntity> AddVoucherAsync(VoucherEntity entity, CancellationToken cancellationToken);
        Task<VoucherEntity> UpdateVoucherAsync(int VoucherId, VoucherEntity entity, CancellationToken cancellationToken);
        Task<bool> DeleteVoucherAsync(int VoucherId, CancellationToken cancellationToken);
    }
}
