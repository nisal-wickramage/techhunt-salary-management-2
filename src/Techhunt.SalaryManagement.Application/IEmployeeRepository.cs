using System;
using System.Collections.Generic;
using System.Text;
using Techhunt.SalaryManagement.Domain;

namespace Techhunt.SalaryManagement.Application
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Get(
            double minSalary, 
            double maxSalary, 
            long offset, 
            int limit, 
            EmployeeSortOptions sort);

        Employee Get(string id);

        void Delete(string id);

        void Create(Employee employee);

        void Create(IEnumerable<Employee> employees);

        void Update(Employee employee);
    }
}
