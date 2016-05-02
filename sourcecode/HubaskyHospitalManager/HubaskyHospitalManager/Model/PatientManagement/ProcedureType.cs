using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HubaskyHospitalManager.Model.PatientManagement;
using HubaskyHospitalManager.Model.InventoryManagement;
using HubaskyHospitalManager.Model.Common;
using HubaskyHospitalManager.Model.HospitalManagement;
using System.ComponentModel.DataAnnotations;

namespace HubaskyHospitalManager.Model.PatientManagement
{
    public class ProcedureType : ICloneable
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }

        public bool AllowedDoctor { get; set; }
        public bool AllowedNurse { get; set; }
        public bool AllowedLaboratorian { get; set; }
        public bool AllowedDataRecorder { get; set; }
        public bool AllowedAdministrator { get; set; }

        public virtual List<Ward> Wards { get; set; }

        public ProcedureType()
        {
            Wards = new List<Ward>();
        }

        public ProcedureType(string id, string name, List<Role> allowedRoles)
        {
            Wards = new List<Ward>();
            Id = id;
            Name = name;
            
            AllowedDoctor = false;
            AllowedNurse = false;
            AllowedLaboratorian = false;
            AllowedDataRecorder = false;
            AllowedAdministrator = false;

            foreach (Role role in allowedRoles)
            {
                switch (role)
                {
                    case Role.Administrator:
                        AllowedAdministrator = true;
                        break;
                    case Role.DataRecorder:
                        AllowedDataRecorder = true;
                        break;
                    case Role.Doctor:
                        AllowedDoctor = true;
                        break;
                    case Role.Laboratorian:
                        AllowedLaboratorian = true;
                        break;
                    case Role.Nurse:
                        AllowedNurse = true;
                        break;
                }
            }
        }


        public static List<Role> GenerateRoles(ProcedureType procedure)
        {
            List<Role> list = new List<Role>();

            if (procedure.AllowedAdministrator)
                list.Add(Role.Administrator);
            if (procedure.AllowedDataRecorder)
                list.Add(Role.DataRecorder);
            if (procedure.AllowedDoctor)
                list.Add(Role.Doctor);
            if (procedure.AllowedLaboratorian)
                list.Add(Role.Laboratorian);
            if (procedure.AllowedNurse)
                list.Add(Role.Nurse);

            return list;
        }

        public object Clone()
        {
            ProcedureType clone = new ProcedureType();

            clone.Id = this.Id;
            clone.Name = this.Name;
            clone.Wards = this.Wards;

            clone.AllowedAdministrator = this.AllowedAdministrator;
            clone.AllowedDataRecorder = this.AllowedDataRecorder;
            clone.AllowedDoctor = this.AllowedDoctor;
            clone.AllowedLaboratorian = this.AllowedLaboratorian;
            clone.AllowedNurse = this.AllowedNurse;

            return clone;
        }
    }
}