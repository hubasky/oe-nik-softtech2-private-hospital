///////////////////////////////////////////////////////////
//  Unit.cs
//  Implementation of the Class Unit
//  Generated by Enterprise Architect
//  Created on:      07-�pr.-2016 12:45:19
//  Original author: justo
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



using HospitalManagement;
namespace HospitalManagement {
	public class Unit {

		private string name;
		private HospitalManagement.Employee manager;
		private List<Employee> employees;

		public Unit(){

		}

		~Unit(){

		}

		public string Name{
			get{
				return name;
			}
		}

		public Employee Manager{
			get;
			set;
		}

		public List<Employee> Employees{
			get;
			set;
		}

	}//end Unit

}//end namespace HospitalManagement