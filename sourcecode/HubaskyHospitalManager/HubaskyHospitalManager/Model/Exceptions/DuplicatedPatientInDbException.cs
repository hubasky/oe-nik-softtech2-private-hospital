using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubaskyHospitalManager.Model.Exceptions
{
    public class DuplicatedPatientInDbException : Exception
    {
        public DuplicatedPatientInDbException()
        {
        }

        public DuplicatedPatientInDbException(string message) : base(message)
        {
        }

        public DuplicatedPatientInDbException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
