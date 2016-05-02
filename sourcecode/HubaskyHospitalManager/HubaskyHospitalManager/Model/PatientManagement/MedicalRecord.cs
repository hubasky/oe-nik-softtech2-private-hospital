﻿//merged with SB 20160430_0920
///////////////////////////////////////////////////////////
//  MedicalRecord.cs
//  Implementation of the Class MedicalRecord
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
using System.ComponentModel.DataAnnotations;

namespace HubaskyHospitalManager.Model.PatientManagement
{
    public class MedicalRecord
    {
        //egy medical history van, azon belül több medical record, azon belül több procedure
        [Key]
        public string CreatedTimestamp { get; private set; }
        public string LastModifiedTimestamp { get; set; }

        //set esetén ezeken keresztül frissül a LastModifiedTimestamp!
        private string anamnesis;
        private string diagnosis;
        private State state;
        private List<Procedure> procedures;

        private string shortDescription;

        public string ShortDescription
        {
            get { return shortDescription; }
            set
            {
                shortDescription = value;
                IsUpdated();
            }
        }

        public string Anamnesis
        {
            get { return anamnesis; }
            set
            {
                anamnesis = value;
                IsUpdated();
            }
        }

        public string Diagnosis
        {
            get { return diagnosis; }
            set
            {
                diagnosis = value;
                IsUpdated();
            }
        }

        public State State
        {
            get { return state; }
            set
            {
                state = value;
                IsUpdated();
            }
        }


        public virtual List<Procedure> Procedures
        {
            get { return procedures; }
            set
            {
                procedures = value;
                IsUpdated();
            }
        }


        public MedicalRecord()
        {
            //Guid g = new Guid();
            this.CreatedTimestamp = DateTime.Now.ToString("yyyyMMddHHmmssff_" + Guid.NewGuid());
            this.State = State.New;
            this.Diagnosis = "";
            this.ShortDescription = "Írjon be a kezelés rövid leírását";
            this.Procedures = new List<Procedure>();
            //this.Procedures.Add(new Procedure()); // <- ez nem jó ötlet itt [SB]

            //setterek miatt a végén!
            this.LastModifiedTimestamp = this.CreatedTimestamp;
        }

        public void NewProcedure(Procedure procedure)
        {
            this.Procedures.Add(procedure);
        }

        public void CloseProcedure(Procedure procedure)
        {
            procedure.IsClosed();
        }

        public void UpdateProcedure(Procedure procedureFromUI, Procedure procedureToDB)
        {
            if (!(procedureFromUI.Equals(procedureToDB)))
            {
                //itt lehetne property-nként is vizsgálni, de nagyon macera, rengeteg equals vizsgálat lenne
                //ugyanakkor egy procedure még nem lehet túl nagy objektum, ezért érdemes egyben kiírni
                procedureToDB = procedureFromUI;
            }
        }

        public List<Procedure> UpdateMedicalRecord(MedicalRecord newMedicalRecord)
        {
            //itt nem érdemes hasonlítani, 0 erőforrás ezeket felülcsapni
            this.CreatedTimestamp = newMedicalRecord.CreatedTimestamp;
            this.Anamnesis = newMedicalRecord.Anamnesis;
            this.Diagnosis = newMedicalRecord.Anamnesis;
            this.State = newMedicalRecord.State;
            this.ShortDescription = newMedicalRecord.ShortDescription;

            //lehetne bonyolultabb algoritmust is írni, de nagyon valószínűtlen, hogy sorrendileg felcserélődnének
            //a listában az objektumok
            for (int i = 0; i < newMedicalRecord.Procedures.Count; i++)
            {
                if (!(this.Procedures[i].Equals(newMedicalRecord.Procedures[i])))
                {
                    //itt nem szelektáljuk, hogy melyik property-ket írja felül, mindent felülcsap, ha non-equal
                    this.Procedures[i] = newMedicalRecord.Procedures[i];
                }
            }

            //fontos, mert minden set felülpecsételné, és nem lenne azonos a temp és a DB objektum stamp-je!
            this.LastModifiedTimestamp = newMedicalRecord.LastModifiedTimestamp;

            return this.Procedures;
        }

        public override bool Equals(object obj)
        {
            var properMedicalRecord = obj as MedicalRecord;

            if (properMedicalRecord == null)
            {
                return false;
            }

            //ha minden set-hez be van írva az isUpdated(), akkor csak ezeket kell ellenőrizni!
            if (!(this.CreatedTimestamp == properMedicalRecord.CreatedTimestamp &&
                this.LastModifiedTimestamp == properMedicalRecord.LastModifiedTimestamp))
            {
                return false;
            }

            return true;
        }

        private void IsUpdated()
        {
            //Guid g = new Guid();
            LastModifiedTimestamp = DateTime.Now.ToString(DateTime.Now.ToString("yyyyMMddHHmmssff_" + Guid.NewGuid()));
        }


        public void IsClosed()
        {
            this.State = State.Closed;
            //itt még végig kell hupákolni a procedure-öket is!
        }

    }//end MedicalRecord

}//end namespace PatientManagement