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
            Object clone = new Patient(
                this.Name,
                this.Phone,
                this.DateOfBirth,
                this.Ssn,
                this.Address,
                this.MedicalHistory,
                this.Gender);

            return clone;
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

        //public override bool Equals(object obj)
        //{
        //    var properPatient = obj as Patient;

        //    if (properPatient == null)
        //    {
        //        return false;
        //    }

        //    //adattagok tesztelése, elvileg lehet equals nélkül ezeket
        //    if (!(this.Phone == properPatient.Phone &&
        //        this.Name == properPatient.Name &&
        //        this.DateOfBirth == properPatient.DateOfBirth &&
        //        this.Ssn == properPatient.Ssn &&
        //        this.Address == properPatient.Address &&
        //        this.Gender == properPatient.Gender))
        //    {

        //        return false;

        //    }

        //    if (!(this.MedicalHistory == null)) //ha nem null 
        //    {
        //        if (properPatient.MedicalHistory == null) //ha null
        //        {
        //            return false;
        //        }
        //        else { 
        //        //ha ugyanolyan hosszúak, akkor fusson csak le, különben hibára futhat
        //            if (this.MedicalHistory.Count == properPatient.MedicalHistory.Count &&
        //                this.MedicalHistory.Count >0 &&
        //                properPatient.MedicalHistory.Count >0)
        //            {
        //                int idx = 0;
        //                while (this.MedicalHistory[idx].Equals(properPatient.MedicalHistory[idx]))
        //                {
        //                    idx++;
        //                }

        //                if (!(idx == this.MedicalHistory.Count))
        //                {
        //                    return false;
        //                }
        //                //kilép, true
        //            }
        //        }
        //    }

        //    //ha minden stimmel
        //    return true;
        //}


        ////CSAK akkor hívják, ha a betegnek nincs nyitott medicalrecordja, és új procedure-t adna valaki hozzá...
        ////ekkor lefut a new medical history, de annak az adattagjának kéne egy adattagját return-ölni
        ////Demeter pedig ezt nem szereti
        //public Procedure ReturnFirstProcedureOfNewMedicalRecord()
        //{
        //    return this.MedicalHistory[0].Procedures[0];
        //}

    }//end Patient

}//end namespace PatientManagement