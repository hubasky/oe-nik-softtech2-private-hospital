//merged with SB 20160430_0920
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
            PopulateDb.PopulateEmployees(appMgr);
            PopulateDb.PopulateUnits(appMgr);
            PopulateDb.PopulatePatients(appMgr);
        }


        private static void PopulatePatients(ApplicationManager appMgr)
        {

            var pats = appMgr.ApplicationDb.Patients.Select(m => m);
            appMgr.ApplicationDb.Patients.RemoveRange(pats);

            Patient kati = new Patient(
                "Kómás Kati",   //name
                "069011111",       //tel
                "1996.06.20",   //DoB
                9991999,         //SSN
                null,           //medicalHistory
                Gender.Female   //Gender
                );

            Patient karesz = new Patient(
                "Kankós Karesz",   //name
                "0660222222",       //tel
                "1991.02.13",   //DoB
                1359143,         //SSN
                null,           //medicalHistory
                Gender.Male   //Gender
                );

            Patient imre = new Patient(
                "Itókás Imre",   //name
                "0670333333",       //tel
                "1978.09.06",   //DoB
                9083825,         //SSN
                null,           //medicalHistory
                Gender.Male   //Gender
                );

            Patient hilda = new Patient(
                "Hasmenős Hilda",   //name
                "062044444",        //tel
                "1957.11.12",       //DoB
                1125267,            //SSN
                null,               //medicalHistory
                Gender.Female       //Gender
                );

            Patient bertold = new Patient(
                "Barangoló Bertold",//name
                "064055555",        //tel
                "1985.02.16",       //DoB
                9306484,            //SSN
                null,               //medicalHistory
                Gender.Male         //Gender
                );


            List<Patient> patList = new List<Patient>();
            for (int i = 0; i < 80; i++)
            {
                patList.Add(new Patient("Teszt" + i, "0660" + i, "1980.XX." + i,
                    9989110 + i, null, (Gender)(i % 2)));
            }

            appMgr.ApplicationDb.Patients.Add(kati);
            appMgr.ApplicationDb.Patients.Add(karesz);
            appMgr.ApplicationDb.Patients.Add(imre);
            appMgr.ApplicationDb.Patients.Add(hilda);
            appMgr.ApplicationDb.Patients.Add(bertold);

            appMgr.ApplicationDb.Patients.AddRange(patList);

            appMgr.ApplicationDb.SaveChanges();

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

            List<Employee> empList = new List<Employee>();  
            for (int i = 0; i < 80; i++)
            { 
                empList.Add(new Employee("Teszt" + i, "1234", 100000 + i, Role.Doctor, "teszt" + i, "teszt" + i, "teszt" + i));
            }

            appMgr.ApplicationDb.Employees.Add(pista);
            appMgr.ApplicationDb.Employees.Add(margit);
            appMgr.ApplicationDb.Employees.Add(laci);
            appMgr.ApplicationDb.Employees.Add(eszti);
            appMgr.ApplicationDb.Employees.Add(andris);
            appMgr.ApplicationDb.Employees.AddRange(empList);

            //ha itt megakad, bump-olni kell a verziószámot, vagy kikapcsolni a populate-et!
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
            //appMgr.ApplicationDb.Departments.Add(patientTreatDept);

            Department surgeryDept = new Department();
            surgeryDept.Name = "Sebészeti főosztály";
            hospital.Departments.Add(surgeryDept);
            //appMgr.ApplicationDb.Departments.Add(surgeryDept);

            Department testDept = new Department();
            testDept.Name = "Teszt főosztály";
            hospital.Departments.Add(testDept);
            //appMgr.ApplicationDb.Departments.Add(test);

            List<Department> deptList = new List<Department>();
            deptList.Add(patientTreatDept);
            deptList.Add(surgeryDept);
            deptList.Add(testDept);
            appMgr.ApplicationDb.Departments.AddRange(deptList);

            XmlReader xmlReader = new XmlReader();
            int length = xmlReader.Wards.Count();
            List<Ward> wardList = new List<Ward>();
            List<Employee> empList = new List<Employee>();
                
            for (int i = 0; i < length-1; i++)
            {
                wardList.Add(new Ward(i, xmlReader.Wards.ElementAt(i), 
                    emps[i % 5], emps.Where(x => x.Username.Equals("Teszt" + i)).ToList(), 
                    xmlReader.Procedures));
            }

            appMgr.ApplicationDb.Wards.Union(wardList);
            patientTreatDept.Wards.AddRange(wardList.GetRange(0, 8));
            surgeryDept.Wards.AddRange(wardList.GetRange(8, 8));
            testDept.Wards.AddRange(wardList.GetRange(16, 8));

            /*Ward walkInPatientWard = new Ward();
            walkInPatientWard.Name = "Járóbetegellátás";
            walkInPatientWard.Manager = emps[0];
            walkInPatientWard.Employees = emps;
            appMgr.ApplicationDb.Wards.Add(walkInPatientWard);

            Ward cardiologyWard = new Ward();
            cardiologyWard.Name = "Kardiológia";
            cardiologyWard.Manager = emps[1];
            appMgr.ApplicationDb.Wards.Add(cardiologyWard);*/

            /*patientTreatDept.Wards.Add(walkInPatientWard);
            patientTreatDept.Wards.Add(cardiologyWard);*/

            appMgr.ApplicationDb.Hospital.Add(hospital);

            appMgr.ApplicationDb.SaveChanges();
        }
    }
}
