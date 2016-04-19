using HubaskyHospitalManager.Model.ApplicationManagement;
using HubaskyHospitalManager.Model.Common;
using HubaskyHospitalManager.Model.HospitalManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HubaskyHospitalManager.Data
{
    public static class PopulateDb
    {
        public static void Populate(ApplicationManager appMgr)
        {
            PopulateDb.PopulateUnits(appMgr);
            //PopulateDb.PopulateEmployees(appMgr);

            
            
        }

        private static void PopulateEmployees(ApplicationManager appMgr)
        {
            var emps = appMgr.ApplicationDb.Employees.Select(m => m);

            appMgr.ApplicationDb.Employees.RemoveRange(emps);

            Employee pista = new Employee("pista", "1234");
            pista.Name = "Istápoló István";
            pista.Phone = "+3630111";
            pista.Salary = 250000;
            pista.Role = Role.Doctor;
            pista.DateOfBirth = "1980.05.21";

            Employee margit = new Employee("margit", "1234");
            margit.Name = "Minta Margit";
            margit.Phone = "+3630112";
            margit.Salary = 150000;
            margit.Role = Role.Nurse;
            margit.DateOfBirth = "1972.09.13";

            Employee laci = new Employee("laci", "1234");
            laci.Name = "Laboráns László";
            laci.Phone = "+3630113";
            laci.Salary = 200000;
            laci.Role = Role.Laboratorian;
            laci.DateOfBirth = "1986.01.06";

            Employee eszti = new Employee("eszti", "1234");
            eszti.Name = "Eszes Eszter";
            eszti.Phone = "+3630114";
            eszti.Salary = 180000;
            eszti.Role = Role.Administrator;
            eszti.DateOfBirth = "1981.11.26";

            Employee andris = new Employee("andris", "1234");
            andris.Name = "Adatrögzítő András";
            andris.Phone = "+3630115";
            andris.Salary = 120000;
            andris.Role = Role.DataRecorder;
            andris.DateOfBirth = "1972.10.08";

            appMgr.ApplicationDb.Employees.Add(pista);
            appMgr.ApplicationDb.Employees.Add(margit);
            appMgr.ApplicationDb.Employees.Add(laci);
            appMgr.ApplicationDb.Employees.Add(eszti);
            appMgr.ApplicationDb.Employees.Add(andris);

            appMgr.ApplicationDb.SaveChanges();
        }

        private static void PopulateUnits(ApplicationManager appMgr)
        {
            Employee hospMgr = null;
            
            var hosps = appMgr.ApplicationDb.Hospital.Select(m => m);
            foreach (Hospital hosp in hosps)
            {
                hosp.Departments = new List<Department>();
            }
            var wards = appMgr.ApplicationDb.Wards.Select(w => w);
            appMgr.ApplicationDb.Wards.RemoveRange(wards);
            var depts = appMgr.ApplicationDb.Departments.Select(d => d);
            appMgr.ApplicationDb.Departments.RemoveRange(depts);

            appMgr.ApplicationDb.Hospital.RemoveRange(hosps);

            var emps = appMgr.ApplicationDb.Employees.Select(m => m).ToList();
            Hospital hospital = new Hospital();
            hospital.Address = "1234 Budapest, Gyógyító köz 1.";
            foreach (Employee emp in emps)
            {
                if ("eszti".Equals(emp.Username))
                {
                    hospMgr = emp;
                }
            }
            hospital.Manager = hospMgr;
            hospital.Name = "Hubasky Magánkórház";
            hospital.Employees = emps;

            Department patientTreatDept = new Department();
            patientTreatDept.Name = "Betegellátási főosztály";
            hospital.Departments.Add(patientTreatDept);
            appMgr.ApplicationDb.Departments.Add(patientTreatDept);

            Ward walkInPatientWard = new Ward();
            walkInPatientWard.Name = "Járóbetegellátás";
            walkInPatientWard.Manager = emps[0];
            walkInPatientWard.Employees = emps;
            appMgr.ApplicationDb.Wards.Add(walkInPatientWard);

            Ward cardiologyWard = new Ward();
            cardiologyWard.Name = "Kardiológia";
            cardiologyWard.Manager = emps[1];
            appMgr.ApplicationDb.Wards.Add(cardiologyWard);

            patientTreatDept.Wards.Add(walkInPatientWard);
            patientTreatDept.Wards.Add(cardiologyWard);
            appMgr.ApplicationDb.Hospital.Add(hospital);

            appMgr.ApplicationDb.SaveChanges();
        }
    }
}
