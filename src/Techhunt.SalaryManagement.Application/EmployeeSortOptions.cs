using System;
using System.Collections.Generic;
using System.Text;

namespace Techhunt.SalaryManagement.Application
{
    public struct EmployeeSortOptions
    {
        public Field Field { get; set; }

        public Order Order { get; set; }
    }

    public enum Field
    { 
        Id,
        Login,
        Name,
        Salary
    }
    public enum Order
    {
        Asc,
        Desc
    }
}
