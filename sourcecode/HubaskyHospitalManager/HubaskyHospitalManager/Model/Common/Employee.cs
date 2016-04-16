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
using System.ComponentModel.DataAnnotations;

namespace HubaskyHospitalManager.Model.Common
{
	public class Employee : Person, ICloneable
    {
        [Key]
        public string Username { get; private set; }
        public string Password { get; private set; }
        public double Salary { get; set; }
        public Role Role { get; set; }
        public Ward Ward { get; set; }

        public Employee()
        {
        }

		public Employee(string username, string password)
        {
            this.Username = username;
            this.Password = password;
		}

        public object Clone()
        {
            Employee clone = new Employee(this.Username, this.Password);
            clone.Name = this.Name;
            clone.Phone = this.Phone;
            clone.Salary = this.Salary;
            clone.Role = this.Role;
            clone.Ward = this.Ward;
            return clone;
        }
    }
}