using System.Collections.Generic;
using System.IO;
using Techhunt.SalaryManagement.Domain;

namespace Techhunt.SalaryManagement.Application
{
    public interface ICsvMapper
    {
        IEnumerable<Employee> GetEmployees(MemoryStream stream);
    }
}
