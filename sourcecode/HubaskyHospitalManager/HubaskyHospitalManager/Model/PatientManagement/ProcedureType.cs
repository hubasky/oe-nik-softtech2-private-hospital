using HubaskyHospitalManager.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubaskyHospitalManager.Model.PatientManagement
{
    public class ProcedureType
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual List<Role> AllowedRoles { get; set; }

        public ProcedureType(string id, string name, List<Role> allowedRoles)
        {
            Id = id;
            Name = name;
            AllowedRoles = allowedRoles;
        }
    }
}
