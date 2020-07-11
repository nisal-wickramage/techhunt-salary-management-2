using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Techhunt.SalaryManagement.Application;
using Techhunt.SalaryManagement.Domain;

namespace Techhunt.SalaryManagement.Infrastructure.Csv
{
    public class CsvMapper : ICsvMapper
    {
        public IEnumerable<Employee> GetEmployees(MemoryStream stream)
        {
            stream.Position = 0;
            IEnumerable<Employee> employees;
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.RegisterClassMap<CsvEmployeeMap>();
                csv.Configuration.Comment = '#';
                csv.Configuration.AllowComments = true;

                employees = csv.GetRecords<Employee>().ToList(); 
            }
            return employees;
        }
    }
}
