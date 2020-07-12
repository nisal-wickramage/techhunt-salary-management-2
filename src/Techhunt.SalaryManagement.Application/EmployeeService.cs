using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Techhunt.SalaryManagement.Domain;

namespace Techhunt.SalaryManagement.Application
{
    public class EmployeeService
    {
        private IEmployeeRepository _repository;

        private ICsvMapper _csvReader;

        public EmployeeService(
            IEmployeeRepository repository,
            ICsvMapper csvReader)
        {
            _repository = repository;
            _csvReader = csvReader;
        }


        public async Task Create(Employee employee)
        {
            var user = await _repository.Get(employee.Id);
            if (user != null)
            {
                throw new InvalidEmployeeDataException("Employee already exists.");
            }
            await _repository.Create(employee);
        }

        public async Task Create(MemoryStream csvStream)
        {
            var employees = _csvReader.GetEmployees(csvStream);
            employees.AssertNonZeroRecords();
            employees.AssertValidIndividualRecords();
            employees.AssertNoDuplicateRecords();

            await _repository.Create(employees);
        }

        public async Task Delete(string id)
        {
            var user = await Get(id);
            if (user is null)
            {
                throw new InvalidEmployeeDataException("Employee does not exists.");
            }
            await _repository.Delete(id);
        }

        public async Task<IEnumerable<Employee>> Get(
            decimal minSalary,
            decimal maxSalary,
            int offset,
            int limit,
            EmployeeSortOptions sort)
        {
            return await _repository.Get(minSalary, maxSalary, offset, limit, sort);
        }

        public async Task<Employee> Get(string id)
        {
            var user = await _repository.Get(id);
            if (user is null)
            {
                throw new InvalidEmployeeDataException("Employee does not exists.");
            }
            return user;
        }

        public async Task Update(Employee employee)
        {
            var user = await Get(employee.Id);
            if (user is null)
            {
                throw new InvalidEmployeeDataException("Employee does not exists.");
            }
            await _repository.Update(employee);
        }
    }
}
