using HubaskyHospitalManager.Model.ApplicationManagement;
using HubaskyHospitalManager.Model.Common;
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

        public EditPatientWindow()
        {
            InitializeComponent();
            DataContext = this;
            Patient = new Patient();
            IsEdit = false;
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
                missingData += "  név" + Environment.NewLine;                
                validate = false;
            }
            if (Patient.Phone == "")
            {
                missingData += "  telefonszám" + Environment.NewLine;
                validate = false;
            }
            if (Patient.Address == "")
            {
                missingData += "  lakcím" + Environment.NewLine;
                validate = false;
            }
            if (Patient.DateOfBirth == "")
            {
                missingData += "  születési dátum" + Environment.NewLine;
                validate = false;
            }
            else
                Patient.Password = ApplicationManager.CalculateSHA256(Patient.DateOfBirth);
            if (Patient.Ssn == "")
            {
                missingData += "  TAJ szám" + Environment.NewLine;    
                validate = false;
            }

            if (validate)
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Hiányzó adatok:" + Environment.NewLine + missingData, "Hiányzó adatok", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
