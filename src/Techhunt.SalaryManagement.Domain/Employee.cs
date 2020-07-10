using System;
using System.Collections.Generic;
using System.Text;

namespace Techhunt.SalaryManagement.Domain
{
    public class Employee
    {
        public string Id { get; set; }

        public string Login { get; set; }

        public string Name { get; set; }

        public decimal Salary { get; set; }
    }
}
