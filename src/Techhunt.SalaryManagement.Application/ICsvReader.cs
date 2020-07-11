using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Techhunt.SalaryManagement.Domain;

namespace Techhunt.SalaryManagement.Application
{
    public interface ICsvReader
    {
        IEnumerable<Employee> GetEmployees(MemoryStream stream);
    }
}
