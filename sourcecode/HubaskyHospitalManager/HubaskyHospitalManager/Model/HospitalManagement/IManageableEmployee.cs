///////////////////////////////////////////////////////////
//  IManageableEmployee.cs
//  Implementation of the Interface IManageableEmployee
//  Generated by Enterprise Architect
//  Created on:      07-�pr.-2016 12:45:17
//  Original author: Peet
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HubaskyHospitalManager.Model.HospitalManagement;
using HubaskyHospitalManager.Model.Common;

namespace HubaskyHospitalManager.Model.HospitalManagement
{
	public interface IManageableEmployee  {

		/// 
		/// <param name="employee"></param>
		/// <param name="ward"></param>
		void AddEmployee(Employee employee, Unit unit);
        
		/// 
		/// <param name="employee"></param>
		void RemoveEmployee(Employee employee);

		/// 
		/// <param name="sourceEmployee"></param>
		/// <param name="targetEmployee"></param>
		void UpdateEmployee(Employee sourceEmployee, Employee targetEmployee);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="sourceUnit"></param>
        /// <param name="targetUnit"></param>
        void MoveEmployee(Employee employee, Unit sourceUnit, Unit targetUnit);
	}
}