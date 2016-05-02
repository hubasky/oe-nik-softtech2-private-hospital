using System;
using System.Collections.Generic;
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
using HubaskyHospitalManager.Model.ApplicationManagement;
using HubaskyHospitalManager.Model.PatientManagement;

namespace HubaskyHospitalManager.View
{
    /// <summary>
    /// Interaction logic for PatientManagementWindow.xaml
    /// </summary>
    public partial class PatientManagementWindow : Window, INotifyPropertyChanged
    {
        PatientManagementView VM { get; set; }

        public PatientManagementWindow(PatientManager patientmanager)
        {
            InitializeComponent();

            //for (int i = 0; i < 5; i++)
            //{
            //    Console.WriteLine(i + ". páciens 0. medicalrecordja: " + patientmanager.Patients[i].MedicalHistory[0].CreatedTimestamp);
            //}

            VM = new PatientManagementView(patientmanager);

            DataContext = VM;
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    DataContext = VM;

        //}

        private void Btn_NewProcedure_Click(object sender, RoutedEventArgs e)
        {
            ProcedureWindow procedureView = new ProcedureWindow();
            procedureView.ShowDialog();
        }




        private void Btn_NewMedicalRecord_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_ItemUsageMod_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_AttachmentsMod_Click(object sender, RoutedEventArgs e)
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }

        private void Btn_NewPatient_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tbName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsLetter(e.Text[0]) && e.Text != " ";
            VM.FillPatients();
            OnPropertyChanged();
        }

        private void tbFindByName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            VM.FillPatients();
            OnPropertyChanged();
        }









    }
}
