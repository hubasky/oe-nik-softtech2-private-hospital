﻿using HubaskyHospitalManager.Model.Common;
using HubaskyHospitalManager.Model.PatientManagement;
using HubaskyHospitalManager.Model.InventoryManagement;
using HubaskyHospitalManager.Model.HospitalManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HubaskyHospitalManager.View
{
    public class PatientManagementView : INotifyPropertyChanged
    {
        private PatientManager patientmanager;

        private ObservableCollection<Patient> patients;
        public ObservableCollection<Patient> Patients
        {
            get { return patients; }
            set { patients = value; OnPropertyChanged(); }
        }
        
        private Patient selectedPatient;
        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set { selectedPatient = value; OnPropertyChanged(); }
        }

        private string findPatientBySSN = "";
        public string FindPatientBySSN
        {
            get { return findPatientBySSN; }
            set { findPatientBySSN = value; OnPropertyChanged(); FillPatients(); }
        }
        private string findPatientByName = "";
        public string FindPatientByName
        {
            get { return findPatientByName; }
            set { findPatientByName = value; OnPropertyChanged(); FillPatients(); }
        }

        private InventoryManager inventoryManager;
        private HospitalManager hospitalManager;



        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        
        public PatientManagementView(PatientManager patientmanager)
        {
            Patientmanager = patientmanager;
            HospitalManager = patientmanager.AppManager.HospitalManagement;
            InventoryManager = patientmanager.AppManager.InventoryManagement;

            FillPatients();
        }
        

        public void FillPatients()
        {
            ObservableCollection<Patient> newPatients = new ObservableCollection<Patient>();

            if (Patientmanager.Patients != null)
                foreach (Patient pt in Patientmanager.Patients)
                    if (pt.Name.ToLower().Contains((string)FindPatientByName.ToLower()) && pt.Ssn.ToLower().Contains((string)FindPatientBySSN.ToLower()))
                        newPatients.Add(pt);

            Patients = newPatients;
        }

        

        public HospitalManager HospitalManager
        {
            get { return hospitalManager; }
            set { hospitalManager = value; }
        }

        public InventoryManager InventoryManager
        {
            get { return inventoryManager; }
            set { inventoryManager = value; }
        }

        public PatientManager Patientmanager
        {
            get { return patientmanager; }
            set { patientmanager = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
           
        }
    }
}