using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Techhunt.SalaryManagement.Domain;

namespace Techhunt.SalaryManagement.Infrastructure.Persistance
{
    public class EmployeeDbModel : Employee
    {
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
