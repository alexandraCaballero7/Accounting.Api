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
        public async Task<IEnumerable<EmployeeEntity>>GetEmployees()
        { 
            return await dbContext.Employees.ToListAsync();
        }

        public async Task<EmployeeEntity> GetEmployeeByIdAsync(Guid EmployeeId)
        {
            return await dbContext.Employees.FirstOrDefaultAsync(x=>x.Id == EmployeeId);
        }

        public async Task<EmployeeEntity> AddEmployeAsync(EmployeeEntity entity)
        {
           entity.Id = Guid.NewGuid();
          dbContext.Employees.Add(entity);
          
          await dbContext.SaveChangesAsync();
          return entity;
        }

        public async Task<EmployeeEntity> UpdateEmployeAsync(Guid EmployeeId, EmployeeEntity entity)
        {
           var employee = await dbContext.Employees.FirstOrDefaultAsync(x=> x.Id == EmployeeId);

            if (employee is not  null)
            { 
                employee.FirstName = entity.FirstName;
                employee.LastName = entity.LastName;
                employee.Phone = entity.Phone;
                employee.Email = entity.Email;
                employee.HireDate = entity.HireDate;
                employee.Salary = entity.Salary;

                await dbContext.SaveChangesAsync();
                return employee;
            }

            return entity;
        }

        public async Task<bool> DeleteEmployeAsync(Guid EmployeeId)
        {
            var employee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == EmployeeId);

            if (employee is not null)
            {
                dbContext.Employees.Remove(employee);

                return await dbContext.SaveChangesAsync() > 0;
                
            }

            return false;
        }


    }
}
