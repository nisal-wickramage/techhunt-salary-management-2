using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Techhunt.SalaryManagement.Domain;

namespace Techhunt.SalaryManagement.Application
{
    public interface ICsvMapper
    {
        IEnumerable<Employee> GetEmployees(MemoryStream stream);
    }
}
