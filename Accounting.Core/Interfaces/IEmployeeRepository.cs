using Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeEntity>> GetAllEmployeesAsync(CancellationToken cancellationToken);
        Task<EmployeeEntity> GetEmployeeByIdAsync(int EmployeeId, CancellationToken cancellationToken);
        Task<EmployeeEntity> AddEmployeeAsync(EmployeeEntity entity, CancellationToken cancellationToken);
        Task<EmployeeEntity> UpdateEmployeAsync(int EmployeeId, EmployeeEntity entity,CancellationToken cancellationToken);
        Task<bool> DeleteEmployeeAsync(int EmployeeId, CancellationToken cancellationToken); 
    }
}
