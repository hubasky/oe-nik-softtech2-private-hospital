using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubaskyHospitalManager.Model.Exceptions
{
    public class EmployeeRoleForbiddenException : Exception
    {
        public EmployeeRoleForbiddenException()
        {
        }

        public EmployeeRoleForbiddenException(string message) : base(message)
        {
        }

        public EmployeeRoleForbiddenException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
