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
            EditPatientWindow ptWindow = new EditPatientWindow(this.VM);

            ptWindow.ShowDialog();
            if (ptWindow.DialogResult == true)
            {
                VM.Patientmanager.NewPatient(ptWindow.Patient);
                VM.FillPatients();
                VM.SelectedPatient = ptWindow.Patient;
                lbPatient.Items.Refresh();

            }

        }


        private void lbPatient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            UIElement elem = (UIElement)lbPatient.InputHitTest(e.GetPosition(lbPatient));
            while (elem != lbPatient)
            {
                if (elem is ListBoxItem)
                {
                    object selectedItem = ((ListBoxItem)elem).Content;

                    if (VM.SelectedPatient != null)
                    {
                        EditPatientWindow ptWindow = new EditPatientWindow((Patient)VM.SelectedPatient.Clone(), VM);

                        ptWindow.ShowDialog();
                        if (ptWindow.DialogResult == true)
                        {//itt a view nem értesül a változásról, triggerelni kéne
                            VM.Patientmanager.UpdatePatient(ptWindow.Patient, VM.SelectedPatient);
                            Patient selection = VM.SelectedPatient;
                            VM.SelectedPatient = null;
                            VM.SelectedPatient = selection;
                            lbPatient.Items.Refresh();
                        }

                    }

                    return;
                }
                elem = (UIElement)VisualTreeHelper.GetParent(elem);
            }

        }

        private void Btn_NewMedicalRecord_Click(object sender, RoutedEventArgs e)
        {

            MedicalRecordWindow medicalRecordWindow = new MedicalRecordWindow(VM);

            medicalRecordWindow.ShowDialog();
            if (medicalRecordWindow.DialogResult == true)
            {
                VM.Patientmanager.NewMedicalRecord(VM.SelectedPatient, medicalRecordWindow.MedicalRecord);
                VM.FillMedicalHistory();
                MedicalHistory.Items.Refresh();
            }


            //    VM.Patientmanager.NewPatient(ptWindow.Patient);


        }

        private void MedicalHistory_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            UIElement elem = (UIElement)MedicalHistory.InputHitTest(e.GetPosition(MedicalHistory));
            while (elem != MedicalHistory)
            {
                if (elem is ListBoxItem)
                {
                    object selectedItem = ((ListBoxItem)elem).Content;

                    if (VM.SelectedMedicalRecord != null)
                    {
                        MedicalRecordWindow medicalRecordWindow = new MedicalRecordWindow((MedicalRecord)VM.SelectedMedicalRecord.Clone(), VM);

                        medicalRecordWindow.ShowDialog();
                        if (medicalRecordWindow.DialogResult == true)
                        {
                            //VM.Patientmanager.NewMedicalRecord(VM.SelectedPatient, mr);
                            VM.FillMedicalHistory();
                            MedicalHistory.Items.Refresh();
                        }

                    }

                    return;
                }
                elem = (UIElement)VisualTreeHelper.GetParent(elem);
            }
        }


        private void Btn_NewProcedure_Click(object sender, RoutedEventArgs e)
        {
            ProcedureWindow procedureWindow = new ProcedureWindow(VM);

            procedureWindow.ShowDialog();
            if (procedureWindow.DialogResult == true)
            {

                VM.Patientmanager.NewProcedure(VM.SelectedMedicalRecord, procedureWindow.Procedure);
                VM.FillProcedures();
                Procedures.Items.Refresh();
            }

        }


        private void Procedures_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UIElement elem = (UIElement)Procedures.InputHitTest(e.GetPosition(Procedures));
            while (elem != Procedures)
            {
                if (elem is ListBoxItem)
                {
                    object selectedItem = ((ListBoxItem)elem).Content;

                    if (VM.SelectedProcedure != null)
                    {
                        ProcedureWindow procedureWindow = new ProcedureWindow((Procedure)VM.SelectedProcedure.Clone(), VM);

                        procedureWindow.ShowDialog();
                        if (procedureWindow.DialogResult == true)
                        {
                            VM.FillProcedures();
                            Procedures.Items.Refresh();
                        }

                    }

                    return;
                }
                elem = (UIElement)VisualTreeHelper.GetParent(elem);
            }
        }

    }



}

