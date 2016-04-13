using HubaskyHospitalManager.Model.ApplicationManagement;
using HubaskyHospitalManager.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubaskyHospitalManager.Data
{
    public static class PopulateDb
    {
        public static void Populate(ApplicationManager appMgr)
        {
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
    }
}
