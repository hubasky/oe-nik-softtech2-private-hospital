//merged with SB 20160430_0920
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HubaskyHospitalManager.Model.Common;
using HubaskyHospitalManager.Model.PatientManagement;
using System.ComponentModel.DataAnnotations;

namespace HubaskyHospitalManager.Model.Common
{
    public class Patient : Person, ICloneable
    {
        [Key]
        public int Ssn { get; set; }
        public virtual List<MedicalRecord> MedicalHistory { get; set; }
        public Gender Gender { get; set; }
        //public string Address { get; set; }

        public Patient()
        {
            this.MedicalHistory = new List<MedicalRecord>();
            this.Name = "Írja be a beteg nevét!";

        }


        public Patient(string name, string phone, string dateOfBirth, int ssn, string address, List<MedicalRecord> medicalHistory, Gender gender)
        {
            this.Phone = phone;
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
            this.Ssn = ssn;
            this.Gender = gender;
            this.Address = address;
            this.MedicalHistory = (medicalHistory == null) ?
                new List<MedicalRecord>() :
                medicalHistory;

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

        public object Clone()
        {
            Patient clone = new Patient(
                this.Name,
                this.Phone,
                this.DateOfBirth,
                this.Ssn,
                this.Address,
                this.MedicalHistory,
                this.Gender);

            return clone;
        }

        public override bool Equals(object obj)
        {
            var properPatient = obj as Patient;

            if (properPatient == null)
            {
                return false;
            }

            //adattagok tesztelése, elvileg lehet equals nélkül ezeket
            if (!(this.Phone == properPatient.Phone &&
                this.Name == properPatient.Name &&
                this.DateOfBirth == properPatient.DateOfBirth &&
                this.Ssn == properPatient.Ssn &&
                this.Address == properPatient.Address &&
                this.Gender == properPatient.Gender))
            {

                return false;

            }

            if (!(this.MedicalHistory == null)) //ha nem null 
            {
                if (properPatient.MedicalHistory == null) //ha null
                {
                    return false;
                }
                else { 
                //ha ugyanolyan hosszúak, akkor fusson csak le, különben hibára futhat
                    if (this.MedicalHistory.Count == properPatient.MedicalHistory.Count &&
                        this.MedicalHistory.Count >0 &&
                        properPatient.MedicalHistory.Count >0)
                    {
                        int idx = 0;
                        while (this.MedicalHistory[idx].Equals(properPatient.MedicalHistory[idx]))
                        {
                            idx++;
                        }

                        if (!(idx == this.MedicalHistory.Count))
                        {
                            return false;
                        }
                        //kilép, true
                    }
                }
            }

            //ha minden stimmel
            return true;
        }


        //CSAK akkor hívják, ha a betegnek nincs nyitott medicalrecordja, és új procedure-t adna valaki hozzá...
        //ekkor lefut a new medical history, de annak az adattagjának kéne egy adattagját return-ölni
        //Demeter pedig ezt nem szereti
        public Procedure ReturnFirstProcedureOfNewMedicalRecord()
        {
            return this.MedicalHistory[0].Procedures[0];
        }

    }//end Patient

}//end namespace PatientManagement