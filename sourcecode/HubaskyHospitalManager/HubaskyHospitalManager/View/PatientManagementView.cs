using HubaskyHospitalManager.Model.Common;
using HubaskyHospitalManager.Model.PatientManagement;
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
        private string findPatientByName;
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



        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

        public PatientManagementView(PatientManager patientmanager)
        {
            Patientmanager = patientmanager;


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



            ////DEPRECATED ez működik...
            //ObservableCollection<Patient> patList = new ObservableCollection<Patient>();
            //for (int i = 0; i < 80; i++)
            //{
            //    patList.Add(new Patient("Teszt" + i, "0660" + i, "1980.XX." + i,
            //        9989110 + i, i + ". utca " + 10 * i, null, (Gender)(i % 2)));
            //}
            //Patients = patList;


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
            if (name == "tbFindByName_PreviewTextInput" || name == "tbFindBySSN_PreviewTextInput")
            {
                FillPatients();
            } // nem lesz tőle azonnali a keresés
        }
    }
}
