using Accounting.Core.Entities;
using Accounting.Core.Interfaces;
using Accounting.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Infraestructure.Repositories
{
    public class EmployeeRepository(AccountingDbContext dbContext): IEmployeeRepository
    {
        public async Task<IEnumerable<EmployeeEntity>>GetAllEmployeesAsync(CancellationToken cancellationToken)
        { 
            return await dbContext.Employees.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<EmployeeEntity> GetEmployeeByIdAsync(int EmployeeId, CancellationToken cancellationToken)
        {
            return await dbContext.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == EmployeeId, cancellationToken);
        }

        public async Task<EmployeeEntity> AddEmployeeAsync(EmployeeEntity entity, CancellationToken cancellationToken)
        {
            dbContext.Employees.Add(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<EmployeeEntity> UpdateEmployeAsync(int EmployeeId, EmployeeEntity entity, CancellationToken cancellationToken)
        {
            var employee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == EmployeeId, cancellationToken);

            if (employee is null)
                return null;

            employee.FirstName = entity.FirstName;
            employee.LastName = entity.LastName;
            employee.Phone = entity.Phone;
            employee.Email = entity.Email;
            employee.HireDate = entity.HireDate;
            employee.Salary = entity.Salary;
            employee.Department = entity.Department;
            employee.Position = entity.Position;

            await dbContext.SaveChangesAsync(cancellationToken);
            return employee;
        }

        public async Task<bool> DeleteEmployeeAsync(int EmployeeId, CancellationToken cancellationToken)
        {
            var employee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == EmployeeId, cancellationToken);

            if (employee == null) return false;

            dbContext.Employees.Remove(employee);
            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
