using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Techhunt.SalaryManagement.Domain;

namespace Techhunt.SalaryManagement.Application
{
    public static class EmployeeValidationRules
    {
        public static void AssertValidIndividualRecords(this IEnumerable<Employee> employees)
        {
            var isAllRecordsValid = employees.Where(e => e.IsValid).Any();
            if (!isAllRecordsValid)
            {
                throw new InvalidEmployeeDataException("There are one or more invalid records.");
            }
        }

        public static void AssertNonZeroRecords(this IEnumerable<Employee> employees)
        {
            var isAllRecordsValid = employees.Any();
            if (!isAllRecordsValid)
            {
                throw new InvalidEmployeeDataException("There are no records.");
            }
        }

        public static void AssertNoDuplicateRecords(this IEnumerable<Employee> employees)
        {
            var hasDuplicateIds = employees.GroupBy(e => e.Id).Select(eg => eg).Count() != employees.Count();
            var hasDuplicateLogins = employees.GroupBy(e => e.Login).Select(eg => eg).Count() != employees.Count();

            if (hasDuplicateIds)
            {
                throw new InvalidEmployeeDataException("There are one or more duplicate ids.");
            }

            if (hasDuplicateLogins)
            {
                throw new InvalidEmployeeDataException("There are one or more duplicate logins.");
            }
        }
    }
}
