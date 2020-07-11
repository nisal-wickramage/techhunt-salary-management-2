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

        void Delete(string id);

        void Create(Employee employee);

        void Create(IEnumerable<Employee> employees);

        void Update(Employee employee);

        Task SaveChanges();
    }
}
