using System.Collections.Generic;
using System.Threading.Tasks;
using Techhunt.SalaryManagement.Domain;

namespace Techhunt.SalaryManagement.Application
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> Get(
            decimal minSalary,
            decimal maxSalary, 
            int offset, 
            int limit, 
            EmployeeSortOptions sort);

        Task<Employee> Get(string id);

        Task Delete(string id);

        Task Create(Employee employee);

        Task Create(IEnumerable<Employee> employees);

        Task Update(Employee employee);
    }
}
