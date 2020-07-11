using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Techhunt.SalaryManagement.Application;
using Techhunt.SalaryManagement.Domain;

namespace Techhunt.SalaryManagement.Infrastructure.Csv
{
    public class CsvMapper : ICsvMapper
    {
        public async Task<IEnumerable<Employee>> GetEmployees(MemoryStream stream)
        {
            stream.Position = 0;
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Employee>();
                return records;
            }
        }
    }
}
