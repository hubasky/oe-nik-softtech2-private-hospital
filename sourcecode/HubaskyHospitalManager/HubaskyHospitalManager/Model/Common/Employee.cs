///////////////////////////////////////////////////////////
//  Employee.cs
//  Implementation of the Class Employee
//  Generated by Enterprise Architect
//  Created on:      07-�pr.-2016 12:45:16
//  Original author: Peet
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HubaskyHospitalManager.Model.Common;
using HubaskyHospitalManager.Model.HospitalManagement;

namespace HubaskyHospitalManager.Model.Common
{
	public class Employee : Person
    {
        private int salary;
        private Role role;
        private Ward ward;

        public int Salary
        {
            get { return salary; }
            set { salary = value; }
        }

        public Role Role
        {
            get { return role; }
            set { role = value; }
        }

        public Ward Ward
        {
            get { return ward; }
            set { ward = value; }
        }

		public Employee()
        {

		}


	}//end Employee

}//end namespace HospitalManagement