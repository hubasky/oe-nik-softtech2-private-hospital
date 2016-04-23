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
            string version = "13";
            string dataSource = @"Data Source=(localdb)\MSSQLLocalDB";
            string initialCatalog = @"Initial Catalog=tempdb" + version;
            string security = @"Integrated Security=True";
            string dbFileName = @"AttachDBFilename=D:\Dropbox\Egyetem\4. f�l�v\SzofTech2\FF\sourcecode\HubaskyHospitalManager\HubaskyHospitalManager\Data\tempdb" + version + ".mdf";
            string connStr = string.Format("{0};{1};{2};{3}", dataSource, initialCatalog, security, dbFileName);

            // Ez a db server be�ll�t�sa, a file conn stringet benthagyom arra az esetre, ha k�s�bb kellene...
            //connStr = @"Data Source=193.224.69.39,1433;Initial Catalog=testdb01;User ID=sa;Password=szoftech;Pooling=False";
            ApplicationDb = new HubaskyDataBase(connStr);

            // Ezt most �tmenetileg kikapcsolom, igaz�b�l lassan eljutok arra a szintre a hosp managerrel, hogy appon kereszt�l lehet adatb�zisba hozz�adni usereket meg wardokat. Rem�lem :D
            PopulateDb.Populate(this);
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