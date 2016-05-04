﻿//merged with SB 20160430_0920
///////////////////////////////////////////////////////////
//  PatientManager.cs
//  Implementation of the Class PatientManager
//  Generated by Enterprise Architect
//  Created on:      07-ápr.-2016 12:45:18
//  Original author: Owczarek Artur
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HubaskyHospitalManager.Model.PatientManagement;
using HubaskyHospitalManager.Model.Common;
using HubaskyHospitalManager.Model.Exceptions;
using HubaskyHospitalManager.Model.ApplicationManagement;
//using HubaskyHospitalManager.Model.InventoryManagement;
using System.Linq;
using HubaskyHospitalManager.Data;

namespace HubaskyHospitalManager.Model.PatientManagement
{
    public class PatientManager : IPatientManagement
    {
        private ApplicationManagement.ApplicationManager appManager;
        private InventoryManagement.InventoryManager invManager;

        public InventoryManagement.InventoryManager InvManager
        {
            get { return invManager; }
            set { invManager = value; }
        }

        public virtual List<Patient> Patients { get; set; }

        public ApplicationManagement.ApplicationManager AppManager
        {
            get { return appManager; }
            set { appManager = value; }
        }

        public PatientManager()
        {
            Patients = new List<Patient>();
        }

        public PatientManager(ApplicationManager appMgr)
        {
            this.AppManager = appMgr;
            this.invManager = appMgr.InventoryManagement;
            Patients = new List<Patient>();

            Patients = AppManager.ApplicationDb.Patients.Select(m => m).ToList();

        }

        public void RemovePatient(Patient patient)
        {
            Patients.Remove(patient);
            AppManager.ApplicationDb.Patients.Remove(patient);
            AppManager.ApplicationDb.SaveChanges();
        }

        public void NewPatient(Patient patient)
        {
            if (patient != null)
            {
                var duplicatedPatient = (from p in Patients
                                         where patient.Ssn.Equals(p.Ssn)
                                         select p).FirstOrDefault();
                if (duplicatedPatient == null)
                {
                    Patients.Add(patient);
                    AppManager.ApplicationDb.Patients.Add(patient);
                }
                else
                    throw new DuplicatedPatientInDbException(String.Format("Patient identified by ssn {0} already exists in database.", patient.Ssn));
            }
            AppManager.ApplicationDb.SaveChanges();
        }


        public void UpdateProcedure(Procedure procedureFromUI, Procedure procedureToDB)
        {
            if (procedureFromUI != null && procedureToDB != null)
            {
                procedureToDB.UpdateProcedure(procedureFromUI);
            }
            else
            {
                procedureToDB = procedureFromUI;
            }

            AppManager.ApplicationDb.SaveChanges();
        }

        public void UpdateMedicalRecord(MedicalRecord medicalRecordFromUI, MedicalRecord medicalRecordToDB)
        {
            if (medicalRecordFromUI != null && medicalRecordToDB != null)
            {
                medicalRecordToDB.UpdateMedicalRecord(medicalRecordFromUI);
            }
            AppManager.ApplicationDb.SaveChanges();
        }


        public void UpdatePatient(Patient patientFromUI, Patient patientToDB)
        {
            if (patientFromUI != null && patientToDB != null)
            {
                //patientToDB.Phone = patientFromUI.Phone;
                //patientToDB.Name = patientFromUI.Name;
                //patientToDB.DateOfBirth = patientFromUI.DateOfBirth;
                //patientToDB.Ssn = patientFromUI.Ssn;
                //patientToDB.Address = patientFromUI.Address;
                //patientToDB.Gender = patientFromUI.Gender;
                //patientToDB.UpdateMedicalHistory(patientFromUI.MedicalHistory);
                patientToDB.UpdatePatient(patientFromUI);
            }
            else
            {
                patientToDB = patientFromUI;
            }

            AppManager.ApplicationDb.SaveChanges();

        }


        public void RemoveMedicalRecord(Patient patient, MedicalRecord medicalRecord)
        {   //ha benne van, akkor szedjük ki!
            if (patient.MedicalHistory.Contains(medicalRecord))
                patient.MedicalHistory.Remove(medicalRecord);
            AppManager.ApplicationDb.SaveChanges();
        }

        public MedicalRecord NewMedicalRecord(Patient patient, MedicalRecord medicalRecord)
        {
            patient.MedicalHistory.Add(medicalRecord);
            AppManager.ApplicationDb.SaveChanges();

            return medicalRecord;
        }

        public Procedure NewProcedure(MedicalRecord medicalrecord, Procedure procedure)
        {
            medicalrecord.NewProcedure(procedure);
            AppManager.ApplicationDb.SaveChanges();
            return procedure;

        }

        public void RemoveProcedure(MedicalRecord medicalrecord, Procedure procedure)
        {
            medicalrecord.RemoveProcedure(procedure);
            AppManager.ApplicationDb.SaveChanges();
        }

        public void UpdateAttachments(List<Attachment> filesToSave, List<Attachment> filesToDelete, Procedure procedure)
        {
            MedicalRecord mrec = (from m in appManager.ApplicationDb.MedicalRecords
                                  where m.Procedures.Contains(procedure)
                                  select m).FirstOrDefault();
            Patient patient = (from p in appManager.ApplicationDb.Patients
                               where p.MedicalHistory.Contains(mrec)
                               select p).FirstOrDefault();
            FTPConnection ftp = new FTPConnection("193.224.69.39", "balu", "szoftech", "hubasky/attachments");
            ftp.CreateDirectory(patient.Ssn);
        }

    }//end PatientManager

}//end namespace PatientManagement