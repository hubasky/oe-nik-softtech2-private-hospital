using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubaskyHospitalManager.Model.Exceptions
{
    public class InvalidUnitAdditionException : Exception
    {
        public InvalidUnitAdditionException() { }
        public InvalidUnitAdditionException(string message) : base(message) { }
        public InvalidUnitAdditionException(string message, Exception inner) : base(message, inner) { }
    }
}
