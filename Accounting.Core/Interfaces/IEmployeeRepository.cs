using Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeEntity>> GetEmployees();
        Task<EmployeeEntity> GetEmployeeByIdAsync(Guid EmployeeId);
        Task<EmployeeEntity> AddEmployeAsync(EmployeeEntity entity);
        Task<EmployeeEntity> UpdateEmployeAsync(Guid EmployeeId, EmployeeEntity entity);
        Task<bool> DeleteEmployeAsync(Guid EmployeeId);
    }
}
