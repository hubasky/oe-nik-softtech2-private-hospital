using HubaskyHospitalManager.Model.Common;
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

        public Patient ClonedPatient { get; set; }
        private Patient selectedPatient;
        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                selectedPatient = value;
                if (selectedPatient != null) ClonedPatient = (Patient)selectedPatient.Clone();
                FillMedicalHistory();
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MedicalRecord> medicalHistory;
        public ObservableCollection<MedicalRecord> MedicalHistory
        {
            get { return medicalHistory; }
            set { medicalHistory = value; OnPropertyChanged(); }
        }

        private MedicalRecord clonedMedicalRecord;
        public MedicalRecord ClonedMedicalRecord
        {
            get { return clonedMedicalRecord; }
            set { clonedMedicalRecord = value; OnPropertyChanged(); }
        }
        private MedicalRecord selectedMedicalRecord;
        public MedicalRecord SelectedMedicalRecord
        {
            get { return selectedMedicalRecord; }
            set
            {
                selectedMedicalRecord = value;
                if (selectedMedicalRecord != null) ClonedMedicalRecord = (MedicalRecord)selectedMedicalRecord.Clone();
                FillProcedures();
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Procedure> procedures;
        public ObservableCollection<Procedure> Procedures
        {
            get { return procedures; }
            set { procedures = value; OnPropertyChanged(); }
        }

        //a klón mindig másolata lesz az eredetinek
        private Procedure clonedProcedure;
        public Procedure ClonedProcedure
        {
            get {return clonedProcedure;}
            set {clonedProcedure = value; OnPropertyChanged();}
        }
        private Procedure selectedProcedure;
        public Procedure SelectedProcedure
        {
            get { return selectedProcedure; }
            set
            {
                selectedProcedure = value;
                if (selectedProcedure != null) ClonedProcedure = (Procedure)SelectedProcedure.Clone();
                OnPropertyChanged();
            }
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

        #region Fill methods

        public void FillPatients()
        {
            ObservableCollection<Patient> newPatients = new ObservableCollection<Patient>();

            if (Patientmanager.Patients != null)
                foreach (Patient pt in Patientmanager.Patients)
                    if (pt.Name.ToLower().Contains(FindPatientByName.ToLower()) && pt.Ssn.ToLower().Contains(FindPatientBySSN.ToLower()))
                        newPatients.Add(pt);

            Patients = newPatients;
        }

        public void FillMedicalHistory()
        {
            ObservableCollection<MedicalRecord> newMedicalHistory = new ObservableCollection<MedicalRecord>();

            if (SelectedPatient != null)
                foreach (MedicalRecord med in SelectedPatient.MedicalHistory)
                    newMedicalHistory.Add(med);

            MedicalHistory = newMedicalHistory;
        }

        public void FillProcedures()
        {
            ObservableCollection<Procedure> newProcedures = new ObservableCollection<Procedure>();

            if (SelectedMedicalRecord != null)
                foreach (Procedure proc in SelectedMedicalRecord.Procedures)
                    newProcedures.Add(proc);

            Procedures = newProcedures;
        }

        #endregion

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
