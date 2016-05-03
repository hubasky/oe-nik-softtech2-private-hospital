using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubaskyHospitalManager.Model.Exceptions
{
    public class ClosedMedicalRecordAdditionException : Exception
    {
        public ClosedMedicalRecordAdditionException() { }
        public ClosedMedicalRecordAdditionException(string message) : base(message) { }
        public ClosedMedicalRecordAdditionException(string message, Exception inner) : base(message, inner) { }
    }
}
