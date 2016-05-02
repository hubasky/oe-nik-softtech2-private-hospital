///////////////////////////////////////////////////////////
//  ApplicationManager.cs
//  Implementation of the Class ApplicationManager
//  Generated by Enterprise Architect
//  Created on:      07-�pr.-2016 12:45:16
//  Original author: Owczarek Artur
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HubaskyHospitalManager.Model.ApplicationManagement;
using HubaskyHospitalManager.Model.HospitalManagement;
using HubaskyHospitalManager.Model.InventoryManagement;
using HubaskyHospitalManager.Model.PatientManagement;
using HubaskyHospitalManager.Model.Common;
using HubaskyHospitalManager.Data;
using System.Linq;
using HubaskyHospitalManager.Model.Exceptions;
using System.Security.Cryptography;

namespace HubaskyHospitalManager.Model.ApplicationManagement
{
	public class ApplicationManager : IApplicationManagement 
    {
        public Employee ApplicationUser { get; set; }
        public PatientManager PatientManagement { get; set; }
        public HospitalManager HospitalManagement { get; set; }
        public InventoryManager InventoryManagement { get; set; }

        public HubaskyDataBase ApplicationDb { get; set; }
        
		public ApplicationManager()
        {
            string version = "00";
            string dataSource = @"Data Source=(localdb)\MSSQLLocalDB";
            string initialCatalog = @"Initial Catalog=tempdb" + version;
            string security = @"Integrated Security=True";
            string dbFileName = @"AttachDBFilename=C:\Users\aowczare\Documents\GitHub\HubaskyHospitalManager\HubaskyHospitalManager\Data\tempdb" + version + ".mdf";
            string connStr = string.Format("{0};{1};{2};{3}", dataSource, initialCatalog, security, dbFileName);

            // Ez a db server beállítása, a file conn stringet benthagyom arra az esetre, ha később kellene...
            connStr = @"Data Source=193.224.69.39,1433;Initial Catalog=testdb02;User ID=sa;Password=szoftech;Pooling=False";

            ApplicationDb = new HubaskyDataBase(connStr);

            // Ezt most átmenetileg kikapcsolom, igazából lassan eljutok arra a szintre a hosp managerrel, hogy appon keresztül lehet adatbázisba hozzáadni usereket meg wardokat. Remélem :D
            // PopulateDb.Populate(this);

            // Ha még nem létezik az adatbázis, akkor default inicializáció:
            if (ApplicationDb.Hospital.FirstOrDefault() == null)
            {
                // Default kórház
                Hospital hosp = new Hospital();
                hosp.Name = "Hubasky Magánkórház";
                hosp.Address = "1234 Budapest, Gyógyító tér 1.";
                hosp.Phone = "+36556667788";
                hosp.Email = "info@hubasky.hu";
                hosp.Web = "hubasky.hu";
                ApplicationDb.Hospital.Add(hosp);

                // Default admin user
                Employee admin = new Employee("admin", "1234", 100000.0, Role.Administrator, "Adminisztrátor", "-", "-", "-");
                hosp.Employees.Add(admin);
                ApplicationDb.SaveChanges();
            }

		}

        public String CalculateSHA256(String data)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                foreach (Byte b in hash.ComputeHash(Encoding.UTF8.GetBytes(data)))
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        ///
        /// <param name="user"></param>
        /// <param name="pass"></param>
        public Employee Authenticate(string username, string password)
        {
            Employee loginUser = null;
            var emps = ApplicationDb.Employees.Select(m => m);
            foreach (Employee emp in emps)
            {
                if (username.Equals(emp.Username) && password.Equals(emp.Password))
                {
                    loginUser = (Employee)emp.Clone();
                    break;
                }
            }

            ApplicationUser = loginUser;
			return ApplicationUser;
		}
        
        /// 
        /// <param name="user"></param>
        /// <param name="pass"></param>
        public Employee Authenticate(string username, string password, Role[] roles)
        {
            Employee loginUser = null;
            var emps = ApplicationDb.Employees.Select(m => m);
            foreach (Employee emp in emps)
            {
                if (username.Equals(emp.Username) && password.Equals(emp.Password))
                {
                    foreach (Role role in roles)
                    {
                        if (role == emp.Role)
                            loginUser = (Employee)emp.Clone();
                    }
                    if (loginUser == null)
                    {
                        ApplicationUser = (Employee)emp.Clone();
                        throw new EmployeeRoleForbiddenException(String.Format("It is forbidden for user \"{0}\" to use the selected module.", emp.Username));
                    }
                }
            }

            ApplicationUser = loginUser;
            return ApplicationUser;
        } 
        public void Logout()
        {
            this.ApplicationUser = null;
        }
    }
}