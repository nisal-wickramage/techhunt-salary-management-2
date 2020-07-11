using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Techhunt.SalaryManagement.Domain;

namespace Techhunt.SalaryManagement.Infrastructure.Persistance
{
    public class EmployeeDbModel : Employee
    {
        public EmployeeDbModel()
        {
        }

        public EmployeeDbModel(Employee employee)
        {
            Id = employee.Id;
            Login = employee.Login;
            Name = employee.Name;
            Salary = employee.Salary;
        }

        public byte[] RowVersion { get; set; }
    }
}
