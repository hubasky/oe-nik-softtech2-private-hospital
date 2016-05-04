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
    public class MedicalRecord : ICloneable
    {
        [Key]
        public int Id { get; set; }
        public string CreatedTimestamp { get; set; }
        public string LastModifiedTimestamp { get; set; }
        public State State { get; set; }
        public virtual List<Procedure> Procedures { get; set; }
        public string ShortDescription { get; set; }


        public MedicalRecord()
        {
            this.CreatedTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.State = State.New;
            this.ShortDescription = "";
            this.Procedures = new List<Procedure>();
            this.LastModifiedTimestamp = this.CreatedTimestamp;
        }

        public MedicalRecord(string shortDescription)
        {
            this.CreatedTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.State = State.New;
            this.ShortDescription = shortDescription;
            this.Procedures = new List<Procedure>();
            this.LastModifiedTimestamp = this.CreatedTimestamp;
        }

        public MedicalRecord(string createdTimestamp, State state, string shortDescription, List<Procedure> procedures, string lastModifiedTimestamp)
        {

            this.CreatedTimestamp = createdTimestamp;
            this.State = state;
            this.ShortDescription = shortDescription;
            this.Procedures = procedures;

            //setterek miatt a végén!
            this.LastModifiedTimestamp = lastModifiedTimestamp;
        }

        public void NewProcedure(Procedure procedure)
        {
            Procedures.Add(procedure);
        }

        public void RemoveProcedure(Procedure procedure)
        {
            if (Procedures.Contains(procedure)) Procedures.Remove(procedure);
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

        public void UpdateMedicalRecord(MedicalRecord newMedicalRecord)
        {
            this.CreatedTimestamp = newMedicalRecord.CreatedTimestamp;
            this.State = newMedicalRecord.State;
            this.ShortDescription = newMedicalRecord.ShortDescription;
            this.Procedures = newMedicalRecord.Procedures;
            this.LastModifiedTimestamp = newMedicalRecord.LastModifiedTimestamp;
        }


        private void IsUpdated()
        {
            //Guid g = new Guid();
            LastModifiedTimestamp = DateTime.Now.ToString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }


        public List<string> IsClosed()
        {

            string[] bill = { "", "" }; //végösszeg, számlaszöveg

            List<string> linesOfBill = new List<string>();
            if (this.State != State.Closed) //ha nem lezárt már
            {

                //Proc.isClosed() returns:
                //billitem[0] = this.CreatedTimestamp;      //date
                //billitem[1] = this.ProcedureType.ToString();//Proc.type
                //billitem[2] = this.Price.ToString();        //PRICE


                int sum = 0;


                linesOfBill.Add(string.Format("                                               TÉTELES SZÁMLA"));
                linesOfBill.Add(""); linesOfBill.Add(""); linesOfBill.Add(""); linesOfBill.Add("");

                linesOfBill.Add(string.Format("Dátum                                                   Vizsgálat                                    Ár"));
                linesOfBill.Add(""); linesOfBill.Add("");


                foreach (Procedure proc in Procedures)
                {
                    string[] billitem = proc.isClosed();

                    //végösszeg
                    sum += Convert.ToInt16(billitem[2]);

                    //számla szöveg:
                    linesOfBill.Add(string.Format("{0}                                            {1}                                          {2}",
                        billitem[0], billitem[1], billitem[2]));
                    linesOfBill.Add(""); linesOfBill.Add("");
                }


                linesOfBill.Add(""); linesOfBill.Add("");
                linesOfBill.Add(string.Format("FIZETENDŐ VÉGÖSSZEG:                                                                           {0}", sum.ToString())); //

                this.State = State.Closed;
            }

            return linesOfBill;
        }


        public Object Clone()
        {
            List<Procedure> proxxx = new List<Procedure>();

            foreach (Procedure proc in Procedures)
            {
                proxxx.Add((Procedure)proc.Clone());
            }

            Object clone = new MedicalRecord(
                this.CreatedTimestamp,
                this.State,
                this.ShortDescription,
                proxxx,
                //this.Procedures,
                this.LastModifiedTimestamp);

            return clone;
        }
    }//end MedicalRecord

}//end namespace PatientManagement