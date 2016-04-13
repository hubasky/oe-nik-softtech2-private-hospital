using HubaskyHospitalManager.Model.Common;
using HubaskyHospitalManager.Model.HospitalManagement;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubaskyHospitalManager.Data
{
    public class HubaskyDataBase : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Ward> Wards { get; set; }
    
        public HubaskyDataBase(string connStr) : base(connStr)
        {
        }

    }
}
