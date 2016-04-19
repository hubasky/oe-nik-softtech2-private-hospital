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

        public DbSet<Hospital> Hospital { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Ward> Wards { get; set; }
    
        public HubaskyDataBase(string connStr) : base(connStr)
        {
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Hospital>()
        //          .HasOptional(j => j.Departments)
        //          .WithMany()
        //          .WillCascadeOnDelete(true);
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
