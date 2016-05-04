//merged with SB 20160430_0920
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HubaskyHospitalManager.Model.Common;
using HubaskyHospitalManager.Model.PatientManagement;
using System.ComponentModel.DataAnnotations;
using HubaskyHospitalManager.Model.ApplicationManagement;

namespace HubaskyHospitalManager.Model.Common
{
    public class Patient : Person, ICloneable
    {
        [Key]
        public string Ssn { get; set; }
        public virtual List<MedicalRecord> MedicalHistory { get; set; }
        public Gender Gender { get; set; }
        public string Password { get; set; }
        public string UserGroup { get; private set; }

        public Patient()
        {
            Ssn = "";
            Name = "";
            Address = "";
            Phone = "";
            DateOfBirth = "";
            Gender = Gender.Female;
            this.MedicalHistory = new List<MedicalRecord>();

            UserGroup = "user";
        }


        public Patient(string name, string phone, string dateOfBirth, string ssn, string address, List<MedicalRecord> medicalHistory, Gender gender)
        {
            Phone = phone;
            Name = name;
            DateOfBirth = dateOfBirth;
            Ssn = ssn;
            Gender = gender;
            Address = address;
            MedicalHistory = (medicalHistory == null) ? new List<MedicalRecord>() : medicalHistory;
            Password = ApplicationManager.CalculateSHA256(DateOfBirth);

            UserGroup = "user";
        }

        public List<MedicalRecord> UpdateMedicalHistory(List<MedicalRecord> newMedicalHistory)
        {
            //lehetne bonyolultabb algoritmust is írni, de nagyon valószínűtlen, hogy sorrendileg felcserélődnének
            //a listában az objektumok
            for (int i = 0; i < newMedicalHistory.Count; i++)
            {
                if (!(this.MedicalHistory[i].Equals(newMedicalHistory[i])))
                {
                    this.MedicalHistory[i].UpdateMedicalRecord(newMedicalHistory[i]);
                }
            }

            return this.MedicalHistory;
        }

        public Object Clone()
        {
            List<MedicalRecord> MedRecList = new List<MedicalRecord>();
            foreach (MedicalRecord mr in this.MedicalHistory)
            {
                MedRecList.Add((MedicalRecord)mr.Clone());
            }

            Patient clone = new Patient(
                this.Name,
                this.Phone,
                this.DateOfBirth,
                this.Ssn,
                this.Address,
                MedRecList,
                this.Gender);

            return (object)clone;
        }

        public void UpdatePatient(Patient newPatient)
        {
            this.Name = newPatient.Name;
            this.Phone = newPatient.Phone;
            this.DateOfBirth = newPatient.DateOfBirth;
            this.Ssn = newPatient.Ssn;
            this.Address = newPatient.Address;
            this.UpdateMedicalHistory(newPatient.MedicalHistory);
            this.Gender = newPatient.Gender;
        }

    }
}