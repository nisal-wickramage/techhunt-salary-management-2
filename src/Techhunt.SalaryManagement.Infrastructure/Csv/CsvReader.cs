using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Techhunt.SalaryManagement.Application;
using Techhunt.SalaryManagement.Domain;

namespace Techhunt.SalaryManagement.Infrastructure.Csv
{
    public class CsvReader : ICsvReader
    {
        public IEnumerable<Employee> GetEmployees(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }
}
