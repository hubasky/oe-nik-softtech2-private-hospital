using HubaskyHospitalManager.Model.Common;
using HubaskyHospitalManager.Model.HospitalManagement;
using HubaskyHospitalManager.Model.PatientManagement;
using HubaskyHospitalManager.Model.InventoryManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public DbSet<Patient> Patients { get; set; }
        public DbSet<ProcedureType> ProcedureTypes { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        //public DbSet<InventoryItem> InventoryUsages { get; set; }
        //public DbSet<File> Attachments { get; set; }
        
        // Inventory Table
        public DbSet<InventoryItem> Inventory { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Auto increment id field in Inventory table
            modelBuilder.Entity<InventoryItem>().Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        public HubaskyDataBase(string connStr)
            : base(connStr)
        {

        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    // Auto increment id field in Inventory table
        //    modelBuilder.Entity<Patient>().Property(a =>
        //            a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        //    base.OnModelCreating(modelBuilder);
        //}

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
