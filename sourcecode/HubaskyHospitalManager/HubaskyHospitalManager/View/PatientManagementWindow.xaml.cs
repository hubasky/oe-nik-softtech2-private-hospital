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
using HubaskyHospitalManager.Model.Common;
using System.Collections.ObjectModel;

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

            VM = new PatientManagementView(patientmanager);

            DataContext = VM;
        }


        private void Btn_NewProcedure_Click(object sender, RoutedEventArgs e)
        {


            ProcedureWindow procedureWindow = new ProcedureWindow(VM);
            procedureWindow.ShowDialog();
            if (procedureWindow.DialogResult == true)
            {
                VM.Patientmanager.NewProcedure(VM.SelectedPatient.SelectedMedicalRecord.ModelMedicalRecord, procedureWindow.PView.ModelProcedure);
            }

        }




        private void Btn_NewMedicalRecord_Click(object sender, RoutedEventArgs e)
        {
            MedicalRecord mr = new MedicalRecord();
            VM.SelectedPatient.MedicalHistory.Add(new MedicalRecordView(mr));
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

        private void Btn_NewPatient_Click(object sender, RoutedEventArgs e)
        {
            EditPatientWindow ptWindow = new EditPatientWindow(this.VM);

            ptWindow.ShowDialog();
            if (ptWindow.DialogResult == true)
            {
                VM.Patientmanager.NewPatient(ptWindow.Patient);

                //ejnye, ez itt nem szép:
                VM.Patients = new ObservableCollection<PatientView>();
                foreach (var item in VM.Patientmanager.Patients)
                {

                    VM.Patients.Add(new PatientView(item));
                }
            }

        }
        private void lbPatient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //    private void popupwindowInventoryListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            //{

            UIElement elem = (UIElement)lbPatient.InputHitTest(e.GetPosition(lbPatient));
            while (elem != lbPatient)
            {
                if (elem is ListBoxItem)
                {
                    object selectedItem = ((ListBoxItem)elem).Content;


                    EditPatientWindow ptWindow = new EditPatientWindow(this.VM.SelectedPatient.ModelPatient, this.VM);

                    ptWindow.ShowDialog();
                    if (ptWindow.DialogResult == true)
                    {
                        ////csak edit, ezért nem kell:
                        //VM.Patientmanager.NewPatient(ptWindow.Patient);

                        //ejnye, ez itt nem szép:
                        VM.Patients = new ObservableCollection<PatientView>();
                        foreach (var item in VM.Patientmanager.Patients)
                        {
                            VM.Patients.Add(new PatientView(item));
                        }
                    }



                    return;
                }
                elem = (UIElement)VisualTreeHelper.GetParent(elem);
            }

        }
    }









}

