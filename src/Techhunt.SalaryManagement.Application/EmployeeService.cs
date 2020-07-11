using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Techhunt.SalaryManagement.Domain;

namespace Techhunt.SalaryManagement.Application
{
    public class EmployeeService
    {
        private IEmployeeRepository _repository;

        private ICsvReader _csvReader;

        public EmployeeService(
            IEmployeeRepository repository,
            ICsvReader csvReader)
        {
            _repository = repository;
            _csvReader = csvReader;
        }


        public async Task Create(Employee employee)
        {
            await _repository.Create(employee);
        }

        public async Task Create(MemoryStream csvStream)
        {
            var employees = _csvReader.GetEmployees(csvStream);
            await _repository.Create(employees);
        }

        public async Task Delete(string id)
        {
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
            return await _repository.Get(id);
        }

        public async Task Update(Employee employee)
        {
            await _repository.Update(employee);
        }
    }
}
