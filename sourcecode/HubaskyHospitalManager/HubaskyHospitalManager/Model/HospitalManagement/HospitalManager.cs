///////////////////////////////////////////////////////////
//  HospitalManager.cs
//  Implementation of the Class HospitalManager
//  Generated by Enterprise Architect
//  Created on:      07-�pr.-2016 12:45:16
//  Original author: hazaip
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HubaskyHospitalManager.Model.ApplicationManagement;
using HubaskyHospitalManager.Model.HospitalManagement;
using HubaskyHospitalManager.Model.Common;
using System.Linq;
using System.Windows;
using HubaskyHospitalManager.Model.Exceptions;
using HubaskyHospitalManager.Model.PatientManagement;
using System.Threading;
using HubaskyHospitalManager.Data;

namespace HubaskyHospitalManager.Model.HospitalManagement
{
	public class HospitalManager : IManageableUnit, IManageableEmployee, IManageableProcedures {

        public Hospital Hospital { get; set; }
        public ApplicationManager AppManager { get; set; }

		public HospitalManager(ApplicationManager appMgr)
        {
            this.AppManager = appMgr;
            Hospital = AppManager.ApplicationDb.Hospital.FirstOrDefault();

            //csak els� futtat�sn�l kell bet�lti a cuccost, azt�n ki lehet kommentezni
            //AddWardsAndProceduresToDB();
		}

        private void AddWardsAndProceduresToDB()
        {
            XmlReader xmlReader = new XmlReader();
            List<Role> roles = new List<Role>();
            roles.Add(Role.Administrator);
            roles.Add(Role.DataRecorder);
            roles.Add(Role.Doctor);
            roles.Add(Role.Laboratorian);
            roles.Add(Role.Nurse);
            for (int i = 0; i < xmlReader.Procedures.Count(); i++)
            {
                AppManager.ApplicationDb.ProcedureTypes.Add(new ProcedureType(xmlReader.Procedures.Keys.ElementAt(i), xmlReader.Procedures.Values.ElementAt(i), roles));
            }
            for (int i = 0; i < xmlReader.Wards.Values.Distinct().Count(); i++)
            {
                var name = xmlReader.Wards.Values.Distinct().ElementAt(i);
                AppManager.ApplicationDb.Wards.Add(new Ward(name, new Employee("tesztuser" + i, "1234"), name + "@hubasky.hu", "110" + i, "http://" + name + ".hubasky.hu"));
            }

            AppManager.ApplicationDb.SaveChanges();
        }
        
		/// 
		/// <param name="employee"></param>
		/// <param name="ward"></param>
		public void AddEmployee(Employee employee, Unit unit)
        {
            // If unit is null or Hospital type, the unit needs to be converted to Department
            if (unit == null)
            {
                Hospital.Employees.Add(employee);
                AppManager.ApplicationDb.SaveChanges();
            }
            else
            {
                unit.Employees.Add(employee);
                AppManager.ApplicationDb.SaveChanges();
            }
		}

        /// 
        /// <param name="sourceEmployee"></param>
        /// <param name="targetEmployee"></param>
        public void UpdateEmployee(Employee sourceEmployee, Employee targetEmployee)
        {
            if (sourceEmployee != null && targetEmployee != null)
            {
                bool changed = false;
                
                if (!targetEmployee.Name.Equals(sourceEmployee.Name))
                {
                    targetEmployee.Name = sourceEmployee.Name;
                    changed = true;
                }
                if (!targetEmployee.Phone.Equals(sourceEmployee.Phone))
                {
                    targetEmployee.Phone = sourceEmployee.Phone;
                    changed = true;
                }
                if (!targetEmployee.Address.Equals(sourceEmployee.Address))
                {
                    targetEmployee.Address = sourceEmployee.Address;
                    changed = true;
                }
                if (!targetEmployee.DateOfBirth.Equals(sourceEmployee.DateOfBirth))
                {
                    targetEmployee.DateOfBirth = sourceEmployee.DateOfBirth;
                    changed = true;
                }
                if (!targetEmployee.Password.Equals(sourceEmployee.Password))
                {
                    targetEmployee.Password = sourceEmployee.Password;
                    changed = true;
                }
                if (Math.Abs(targetEmployee.Salary - sourceEmployee.Salary) > 0.01)
                {
                    targetEmployee.Salary = sourceEmployee.Salary;
                    changed = true;
                }
                if (targetEmployee.Role != sourceEmployee.Role)
                {
                    targetEmployee.Role = sourceEmployee.Role;
                    changed = true;
                }

                if (changed)
                {
                    AppManager.ApplicationDb.SaveChanges();
                }

            }
        }

		/// 
		/// <param name="employee"></param>
		public void RemoveEmployee(Employee employee)
        {
            if (Hospital.Manager != null && Hospital.Manager.Equals(employee))
                Hospital.Manager = null;
            Hospital.Employees.Remove(employee);
            foreach (Department dept in Hospital.Departments)
            {
                if (dept.Manager != null && dept.Manager.Equals(employee))
                    dept.Manager = null;
                dept.Employees.Remove(employee);
                foreach (Ward ward in dept.Wards)
                {
                    if (ward.Manager != null && ward.Manager.Equals(employee))
                        ward.Manager = null;
                    ward.Employees.Remove(employee);
                }
            }
            AppManager.ApplicationDb.Employees.Remove(employee);
            AppManager.ApplicationDb.SaveChanges();
		}

        /// 
        /// <param name="unit"></param>
        /// <param name="parentUnit"></param>
        public void AddUnit(Unit unit, Unit parentUnit)
        {
            if (unit == null)
                throw new InvalidUnitAdditionException("Unit to be added to the database can't be null.");

            // If parentUnit is null or Hospital type, the unit needs to be converted to Department
            
            if (parentUnit as Hospital != null)
            {
                Hospital.Departments.Add(new Department(unit));
                AppManager.ApplicationDb.SaveChanges();
            }
            else
            {
                // If the given unit is ward type, the parent unit must be department
                if (parentUnit as Department != null)
                {
                    Department dept = parentUnit as Department;
                    dept.Wards.Add(new Ward(unit));
                    AppManager.ApplicationDb.SaveChanges();
                }
            }
        }
        
        /// 
        /// <param name="sourceUnit"></param>
        /// <param name="targetUnit"></param>
        public void UpdateUnit(Unit sourceUnit, Unit targetUnit)
        {
            if (sourceUnit != null && targetUnit != null)
            {
                bool changed = false; ;
                if (!targetUnit.Name.Equals(sourceUnit.Name))
                {
                    targetUnit.Name = sourceUnit.Name;
                    changed = true;
                }

                if (targetUnit.Manager != null)
                {
                    if (!targetUnit.Manager.Equals(sourceUnit.Manager))
                    {
                        targetUnit.Manager = sourceUnit.Manager;
                        changed = true;
                    }
                }
                else
                {
                    if (sourceUnit.Manager != null)
                    {
                        targetUnit.Manager = sourceUnit.Manager;
                        changed = true;
                    }
                }
                if (!targetUnit.Phone.Equals(sourceUnit.Phone))
                {
                    targetUnit.Phone = sourceUnit.Phone;
                    changed = true;
                }
                if (!targetUnit.Email.Equals(sourceUnit.Email))
                {
                    targetUnit.Email = sourceUnit.Email;
                    changed = true;
                }
                if (!targetUnit.Web.Equals(sourceUnit.Web))
                {
                    targetUnit.Web = sourceUnit.Web;
                    changed = true;
                }
                if (changed)
                    AppManager.ApplicationDb.SaveChanges();
            }
        }

		/// 
		/// <param name="unit"></param>
		public void RemoveUnit(Unit unit)
        {
            Department dept = unit as Department;
            if (dept != null)
            {
                //if (unit.GetType() == AppManager.ApplicationDb.Departments.FirstOrDefault().GetType())
                //{
                    foreach (Ward ward in dept.Wards)
                    {
                        foreach (Employee emp in ward.Employees)
                            Hospital.Employees.Add(emp);
                    }
                    foreach (Employee deptEmp in dept.Employees)
                        Hospital.Employees.Add(deptEmp);
                    
                    AppManager.ApplicationDb.Wards.RemoveRange(dept.Wards);
                    AppManager.ApplicationDb.Departments.Remove(dept);
                    AppManager.ApplicationDb.SaveChanges();
            }
            else
            {
                Ward ward = unit as Ward;
                if (ward != null)
                {
                    Unit parent = FindParentUnit(ward);
                    foreach (Employee emp in ward.Employees)
                        parent.Employees.Add(emp);

                    AppManager.ApplicationDb.Wards.Remove(ward);
                    AppManager.ApplicationDb.SaveChanges();

                }
            }
		}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="sourceUnit"></param>
        /// <param name="targetUnit"></param>
        public void MoveEmployee(Employee employee, Unit sourceUnit, Unit targetUnit)
        { 
            if (employee != null && sourceUnit != null && targetUnit != null)
            {
                if (!sourceUnit.Equals(targetUnit))
                {
                    sourceUnit.Employees.Remove(employee);
                    targetUnit.Employees.Add(employee);
                    AppManager.ApplicationDb.SaveChanges();
                }
            }
        }
        
        /// 
        /// <param name="unit"></param>
        public Unit FindParentUnit(Unit unit)
        {
            Department dept = unit as Department;
            if (dept != null)
                return Hospital;
            var parent = (from department in Hospital.Departments
                          where department.Wards.Contains((Ward)unit)
                          select department).FirstOrDefault();
            return parent;
        }
        
        public List<Unit> GetUnits()
        {
            List<Unit> list = new List<Unit>();
            list.Add(Hospital);
            foreach (Department dept in Hospital.Departments)
            {
                list.Add(dept);
                foreach (Ward ward in dept.Wards)
                {
                    list.Add(ward);
                }
            }
            list = list.OrderBy(o => o.Name).ToList();
            return list;
        }


		public void UpdateDatabase()
        {
            AppManager.ApplicationDb.SaveChanges();
		}

        public void AddNewProcedure(ProcedureType procedureType)
        {
            AppManager.ApplicationDb.ProcedureTypes.Add(procedureType);
            AppManager.ApplicationDb.SaveChanges();
        }

        public void UpdateProcedure(ProcedureType source, ProcedureType target)
        {
            target.AllowedAdministrator = source.AllowedAdministrator;
            target.AllowedDataRecorder = source.AllowedDataRecorder;
            target.AllowedDoctor = source.AllowedDoctor;
            target.AllowedLaboratorian = source.AllowedLaboratorian;
            target.AllowedNurse = source.AllowedNurse;
            target.Name = source.Name;
            AppManager.ApplicationDb.SaveChanges();
        }

        public void DeleteProcedure(ProcedureType procedureType)
        {
            if(procedureType != null)
                foreach (Ward ward in procedureType.Wards)
                    ward.Procedures.Remove(procedureType);
            AppManager.ApplicationDb.ProcedureTypes.Remove(procedureType);
            AppManager.ApplicationDb.SaveChanges();
        }

    }
}