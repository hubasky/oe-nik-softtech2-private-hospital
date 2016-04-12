using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP_EntityTest
{
    public enum Role
    {
        Doctor,
        Nurse,
        Laboratorian
    }
    public class Employee : Person
    {
        public Role Role { get; set; }
    }
}
