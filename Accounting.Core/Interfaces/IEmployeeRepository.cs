using Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeEntity>> GetAllEmployeesAsync();
        Task<EmployeeEntity> GetEmployeeByIdAsync(int EmployeeId);
        Task<EmployeeEntity> AddEmployeeAsync(EmployeeEntity entity);
        Task<EmployeeEntity> UpdateEmployeAsync(int EmployeeId, EmployeeEntity entity);
        Task<bool> DeleteEmployeeAsync(int EmployeeId); 
    }
}
