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

namespace HubaskyHospitalManager.View
{
    public class PatientManagementView : INotifyPropertyChanged
    {
        private PatientManager patientmanager;

        private ObservableCollection<PatientView> patients;
        private PatientView selectedPatient;

        public PatientView PreviouslySelectedPatient { get; set; }
        
        private string findPatientBySSN;
        private string findPatientByName;

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
            //optimalizálatlan, de időhiány...

            //null lesz, ha 0 a hossz, így egyszerűbb kivételt kezelni
            if (FindPatientBySSN != null && FindPatientBySSN.Length == 0)
            {
                FindPatientBySSN = null;
            }
            if (FindPatientByName != null && FindPatientByName.Length == 0)
            {
                FindPatientByName = null;
            }

            //keresés / feltöltés
            Patients = new ObservableCollection<PatientView>();

            if (Patientmanager.Patients != null)
            {
                if (FindPatientByName == null && FindPatientBySSN == null) //nincs keresés, mindkét mező üres
                {
                    foreach (Patient pt in Patientmanager.Patients)
                    {
                        Patients.Add(new PatientView(pt));
                    }

                }
                else //különben 3 eset van: egyik üres, másik üres, egyik sem üres
                {
                    if (FindPatientByName != null && FindPatientBySSN == null) //nincs keresés
                    {
                        foreach (Patient pt in Patientmanager.Patients)
                        {
                            if (pt.Name.ToLower().Contains(FindPatientByName))
                            {

                                Patients.Add(new PatientView(pt));
                            }
                        }

                    }
                    else if (FindPatientByName == null && FindPatientBySSN != null) //nincs keresés
                    {
                        foreach (Patient pt in Patientmanager.Patients)
                        {
                            if (pt.Ssn.ToString().Contains(FindPatientBySSN))
                            {

                                Patients.Add(new PatientView(pt));
                            }
                        }

                    }
                    else //egyik sem null
                    {
                        foreach (Patient pt in Patientmanager.Patients)
                        {
                            if (pt.Ssn.ToString().Contains(FindPatientBySSN) &&
                                pt.Name.ToLower().Contains(FindPatientByName))
                            {

                                Patients.Add(new PatientView(pt));
                            }
                        }

                    }

                }

            }

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

        public string FindPatientBySSN
        {
            get { return findPatientBySSN; }
            set
            {
                findPatientBySSN = value;
                FillPatients();
                OnPropertyChanged();
            }
        }
        public string FindPatientByName
        {
            get { return findPatientByName; }
            set
            {
                //null értéket ne konvertáljon kisbetűssé, mert megfekszi a gyomrát
                findPatientByName = value == null ? null : value.ToLower();
                FillPatients();
                OnPropertyChanged();
            }
        }

        public PatientManager Patientmanager
        {
            get { return patientmanager; }
            set { patientmanager = value; OnPropertyChanged(); }
        }


        public virtual ObservableCollection<PatientView> Patients
        {
            get { return patients; }
            set { patients = value; OnPropertyChanged(); }
        }


        public PatientView SelectedPatient
        {
            get { return selectedPatient; }
            set { selectedPatient = value; OnPropertyChanged(); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
           
        }
    }
}
