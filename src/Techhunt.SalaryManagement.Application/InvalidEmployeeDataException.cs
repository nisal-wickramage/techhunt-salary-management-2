using System;
using System.Collections.Generic;
using System.Text;

namespace Techhunt.SalaryManagement.Application
{
    public class InvalidEmployeeDataException : Exception
    {
        public InvalidEmployeeDataException() 
        {
        }

        public InvalidEmployeeDataException(string message)
            : base(message)
        {
        }

        public InvalidEmployeeDataException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
