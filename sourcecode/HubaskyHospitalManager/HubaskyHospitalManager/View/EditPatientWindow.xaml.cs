using HubaskyHospitalManager.Model.ApplicationManagement;
using HubaskyHospitalManager.Model.Common;
using HubaskyHospitalManager.Model.HospitalManagement;
using HubaskyHospitalManager.Model.PatientManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HubaskyHospitalManager.View
{
    /// <summary>
    /// Interaction logic for EditPatientWindow.xaml
    /// </summary>
    public partial class EditPatientWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }

        private Patient patient;
        public Patient Patient
        {
            get { return patient; }
            set { patient = value; OnPropertyChanged(); }
        }

        public bool IsEdit { get; set; }
        public Array GenderTypes
        {
            get { return Enum.GetNames(typeof(Gender)); }
        }

        public Boolean SSNValidBool { get; set; }
        public PatientManager PMGR { get; set; }
        public EditPatientWindow(PatientManager patientmanager)
        {
            InitializeComponent();
            DataContext = this;
            Patient = new Patient();
            IsEdit = false;
            this.PMGR = patientmanager;
        }

        public EditPatientWindow(Patient patient)
        {
            InitializeComponent();
            DataContext = this;
            Patient = patient;
            IsEdit = true;
            TxtBox_Ssn.IsEnabled = false;
            TxtBox_Gender.IsEnabled = false;
            TxtBox_DateOfBirth.IsEnabled = false;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool validate = true;
            string missingData = "";

            if (Patient.Name == "")
            {
                missingData += "  név hiányzik" + Environment.NewLine;
                validate = false;
            }
            if (Patient.Phone == "")
            {
                missingData += "  telefonszám hiányzik" + Environment.NewLine;
                validate = false;
            }
            if (Patient.Address == "")
            {
                missingData += "  lakcím hiányzik" + Environment.NewLine;
                validate = false;
            }
            if (Patient.DateOfBirth == "")
            {
                missingData += "  születési dátum hiányzik" + Environment.NewLine;
                validate = false;
            }
            else
                Patient.Password = ApplicationManager.CalculateSHA256(Patient.DateOfBirth);

            if (Patient.Ssn == "")
            {
                missingData += "  TAJ szám hiányzik" + Environment.NewLine;
                validate = false;
            }
            else if (!(isSSNValid(Patient.Ssn)))
            {
                validate = false;
                missingData += "  TAJ szám NEM EGYEDI" + Environment.NewLine;
            }

            if (validate)
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Hiányzó/hibás adatok:" + Environment.NewLine + missingData, "Hiányzó adatok", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private bool isSSNValid(string ssn)
        {
            if (PMGR.Patients != null && PMGR.Patients.Count > 0)
            {
                foreach (Patient pt in PMGR.Patients)
                {
                    if (Patient.Ssn != null && Patient.Ssn == ssn)
                    {
                        return false;
                    }
                }
            }

            return true;


        }
    }
}
