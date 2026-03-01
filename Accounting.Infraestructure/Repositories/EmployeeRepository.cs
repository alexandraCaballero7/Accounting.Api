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
        public async Task<IEnumerable<EmployeeEntity>>GetAllEmployeesAsync()
        { 
            return await dbContext.Employees.ToListAsync();
        }

        public async Task<EmployeeEntity> GetEmployeeByIdAsync(int EmployeeId)
        {
            return await dbContext.Employees.FirstOrDefaultAsync(x=>x.Id == EmployeeId);
        }

        public async Task<EmployeeEntity> AddEmployeeAsync(EmployeeEntity entity)
        {
         
          dbContext.Employees.Add(entity);
          
          await dbContext.SaveChangesAsync();
          return entity;
        }

        public async Task<EmployeeEntity> UpdateEmployeAsync(int EmployeeId, EmployeeEntity entity)
        {
           var employee = await dbContext.Employees.FirstOrDefaultAsync(x=> x.Id == EmployeeId);

            if (employee is null)
                return null;
            
                employee.FirstName = entity.FirstName;
                employee.LastName = entity.LastName;
                employee.Phone = entity.Phone;
                employee.Email = entity.Email;
                employee.HireDate = entity.HireDate;
                employee.Salary = entity.Salary;

                await dbContext.SaveChangesAsync();
                return employee;
            
        }

        public async Task<bool> DeleteEmployeeAsync(int EmployeeId)
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
