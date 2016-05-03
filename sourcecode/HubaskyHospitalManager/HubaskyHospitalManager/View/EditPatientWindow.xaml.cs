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


        private PatientManagementView VM;

        public EditPatientWindow(PatientManagementView view)
        {
            InitializeComponent();
            VM = view;
            //DataContext = VM;
            DataContext = this;
            Patient = new Patient();
            //VM.SelectedPatient = new PatientView(Patient);
        }

        public EditPatientWindow(Patient pt, PatientManagementView view)
        {
            InitializeComponent();
            VM = view;
            //DataContext = VM;
            DataContext = this;
            Patient = pt;
            TxtBox_Ssn.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //DataContext = this;
            //if (!editPatient)
            //{
            //    Patient = new Patient();
            //}
            //else
            //{
            //    TextBox_Username.IsEnabled = false;
            //    Label_Username.IsEnabled = false;
            //}
            //List<Unit> list = HospView.HospManager.GetUnits();
            //Units = new ObservableCollection<Unit>();
            //foreach (Unit unit in list)
            //{
            //    Units.Add(unit);
            //}
            //SelectedUnit = HospView.SelectedUnit.Reference;
            //if (SelectedUnit != null && SelectedUnit.Manager.Username.Equals(Patient.Username))
            //    IsManager = true;
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
            

            //private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
            //{
            //    e.Handled = char.IsLetter(e.Text[0]) || e.Text[0] == ' '; // e.Text[0] != ' '
            //}
        }

        //private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    e.Handled = char.IsLetter(e.Text[0]) || e.Text[0] == ' '; // e.Text[0] != ' '
        //}
    }
}
