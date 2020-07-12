using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task Create(Employee employee)
        {
            try
            {
                var employeeRecord = new EmployeeDbModel(employee);
                _dbContext.Add(employeeRecord);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new InvalidEmployeeDataException("One or more employee records are being updated by another operation.", ex);
            }
        }

        public async Task Create(IEnumerable<Employee> employees)
        {
            try
            {
                var employeeRecords = employees.Select(e => new EmployeeDbModel(e));
                await _dbContext.AddRangeAsync(employeeRecords);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new InvalidEmployeeDataException("One or more employee records are being updated by another operation.", ex);
            }
        }

        public async Task Delete(string id)
        {
            try
            {
                var employeeRecord = new EmployeeDbModel { Id = id };
                _dbContext.Attach(employeeRecord);
                _dbContext.Employees.Remove(employeeRecord);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new InvalidEmployeeDataException("One or more employee records are being updated by another operation.", ex);
            }
        }

        public async Task<IEnumerable<Employee>> Get(
            decimal minSalary,
            decimal maxSalary, 
            int offset, 
            int limit, 
            EmployeeSortOptions sort)
        {
            try
            {
                var query = _dbContext.Employees
                    .Where(e => e.Salary <= maxSalary && e.Salary >= minSalary);

                IOrderedQueryable<EmployeeDbModel> orderedQuery;

                orderedQuery = GetOrderedQuery(query, sort);

                var employees =  await orderedQuery.Skip(offset).Take(limit)
                    .Select(e => e as Employee)
                    .ToListAsync();

                return employees;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new InvalidEmployeeDataException("One or more employee records are being updated by another operation.", ex);
            }
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
            try
            {
                var employeeRecord = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id.Equals(id));
                return employeeRecord;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new InvalidEmployeeDataException("One or more employee records are being updated by another operation.", ex);
            }
        }

        public async Task Update(Employee employee)
        {
            try
            {
                var employeeRecord = new EmployeeDbModel(employee);
                _dbContext.Attach(employeeRecord);
                _dbContext.Employees.Update(employeeRecord);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new InvalidEmployeeDataException("One or more employee records are being updated by another operation.", ex);
            }
        }
    }
}
