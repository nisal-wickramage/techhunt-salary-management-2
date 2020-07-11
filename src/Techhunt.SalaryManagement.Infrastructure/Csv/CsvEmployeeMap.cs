using CsvHelper.Configuration;
using Techhunt.SalaryManagement.Domain;

namespace Techhunt.SalaryManagement.Infrastructure.Csv
{
    internal class CsvEmployeeMap : ClassMap<Employee>
    {
        public CsvEmployeeMap()
        {
            Map(m => m.Id).Name("id", "Id", "ID");
            Map(m => m.Login).Name("login", "Login", "LOGIN");
            Map(m => m.Name).Name("name", "Name", "NAME");
            Map(m => m.Salary).Name("salary", "Salary", "SALARY");
        }
    }
}
