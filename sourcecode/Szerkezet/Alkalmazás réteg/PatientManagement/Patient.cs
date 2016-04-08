///////////////////////////////////////////////////////////
//  Patient.cs
//  Implementation of the Class Patient
//  Generated by Enterprise Architect
//  Created on:      07-�pr.-2016 12:45:18
//  Original author: Owczarek Artur
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



using ApplicationManagement;
using PatientManagement;
namespace PatientManagement {
	public class Patient : Person, Person {

		private List<MedicalRecord> medicalHistory;
		private PatientManagement.Gender gender;

		public Patient(){

		}

		~Patient(){

		}

		public List<MedicalRecord> MedicalHistory{
			get;
			set;
		}

		public Gender Gender{
			get;
			set;
		}

	}//end Patient

}//end namespace PatientManagement