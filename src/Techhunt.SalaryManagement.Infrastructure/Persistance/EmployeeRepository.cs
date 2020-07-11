using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Techhunt.SalaryManagement.Application;
using Techhunt.SalaryManagement.Domain;

namespace Techhunt.SalaryManagement.Infrastructure.Persistance
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SalaryManagementDbContext _dbContext;

        public EmployeeRepository(SalaryManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Employee employee)
        {
            var employeeRecord = new EmployeeDbModel(employee);
            _dbContext.Add(employeeRecord);
        }

        public void Create(IEnumerable<Employee> employees)
        {
            var employeeRecords = employees.Select(e => new EmployeeDbModel(e));
            _dbContext.Add(employeeRecords);
        }

        public void Delete(string id)
        {
            var employeeRecord = new EmployeeDbModel { Id = id };
            _dbContext.Attach(employeeRecord);
            _dbContext.Employees.Remove(employeeRecord);
        }

        public async Task<IEnumerable<Employee>> Get(
            decimal minSalary,
            decimal maxSalary, 
            int offset, 
            int limit, 
            EmployeeSortOptions sort)
        {
            var query = _dbContext.Employees
                .Where(e => e.Salary <= maxSalary && e.Salary >= minSalary);

            IOrderedQueryable<EmployeeDbModel> orderedQuery;

            orderedQuery = GetOrderedQuery(query, sort);

            return await orderedQuery.Skip(offset).Take(limit).Select(e => e as Employee).ToListAsync();
        }

        private IOrderedQueryable<EmployeeDbModel> GetOrderedQuery(
            IQueryable<EmployeeDbModel> query,
            EmployeeSortOptions sort)
        {
            if (sort.Order == Order.Asc)
            {
                switch (sort.Field)
                {
                    case Field.Id:
                        return query.OrderBy(e => e.Id);
                    case Field.Login:
                        return query.OrderBy(e => e.Login);
                    case Field.Name:
                        return query.OrderBy(e => e.Name);
                    default:
                        return query.OrderBy(e => e.Salary);
                }
            }
            else
            {
                switch (sort.Field)
                {
                    case Field.Id:
                        return query.OrderByDescending(e => e.Id);
                    case Field.Login:
                        return query.OrderByDescending(e => e.Login);
                    case Field.Name:
                        return query.OrderByDescending(e => e.Name);
                    default:
                        return query.OrderByDescending(e => e.Salary);
                }
            }
        }


        public async Task<Employee> Get(string id)
        {
            var employeeRecord = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id.Equals(id));
            return employeeRecord;
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(Employee employee)
        {
            var employeeRecord = new EmployeeDbModel(employee);
            _dbContext.Attach(employeeRecord);
            _dbContext.Employees.Update(employeeRecord);
        }
    }
}
