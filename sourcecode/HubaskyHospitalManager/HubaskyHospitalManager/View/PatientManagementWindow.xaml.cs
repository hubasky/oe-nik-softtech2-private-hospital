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
using HubaskyHospitalManager.Model.Exceptions;

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



        # region NewPatient || Gombbal hozzáadás
        //PATIENT GOMB
        private void Btn_NewPatient_Click(object sender, RoutedEventArgs e)
        {
            EditPatientWindow ptWindow = new EditPatientWindow();

            ptWindow.ShowDialog();
            if (ptWindow.DialogResult == true)
            {
                VM.Patientmanager.NewPatient(ptWindow.Patient);
                VM.FillPatients();
                VM.SelectedPatient = ptWindow.Patient;
                lbPatient.Items.Refresh();

            }

        }
        #endregion

        #region Patient szerkesztés || Duplaklikk
        //PATIENT DUPLAKLIKK
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
                        EditPatientWindow ptWindow = new EditPatientWindow(VM.ClonedPatient);

                        ptWindow.ShowDialog();
                        if (ptWindow.DialogResult == true)
                        {
                            //VM.Patientmanager.UpdatePatient(ptWindow.Patient, VM.SelectedPatient);
                            Patient selection = VM.ClonedPatient;
                            //VM.SelectedPatient = null;
                            //VM.SelectedPatient = selection;
                            VM.Patientmanager.UpdatePatient(VM.ClonedPatient, VM.SelectedPatient);
                            lbPatient.Items.Refresh();
                            VM.SelectedPatient = null;
                            VM.SelectedPatient = selection;
                        }

                    }

                    return;
                }
                elem = (UIElement)VisualTreeHelper.GetParent(elem);
            }

        }
        #endregion

        #region RemovePatient || Gombbal törlés
        //PATIENT TÖRLÉS
        private void Btn_RemovePatient_Click(object sender, RoutedEventArgs e)
        {
            VM.Patientmanager.RemovePatient(VM.SelectedPatient);
            VM.FillPatients();
            lbPatient.Items.Refresh();
        }
        #endregion

        #region NewMedicalRecord || Gombbal hozzáadás
        // Új Kezelés
        private void Btn_NewMedicalRecord_Click(object sender, RoutedEventArgs e)
        {
            NewMedicalRecordWindow medicalRecordWindow = new NewMedicalRecordWindow();
            medicalRecordWindow.ShowDialog();
            if (medicalRecordWindow.DialogResult == true)
            {
                VM.Patientmanager.NewMedicalRecord(VM.SelectedPatient, medicalRecordWindow.MedicalRecord);
                VM.FillMedicalHistory();
                MedicalHistory.Items.Refresh();
            }
        }
        #endregion

        // Kezelés fizetése
        #region MedicalHistory fizetés || Duplaklikk

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

                        if (VM.SelectedMedicalRecord.State != State.Closed && VM.SelectedMedicalRecord.State != State.Paid)
                        {
                            MedicalRecordWindow medicalRecordWindow = new MedicalRecordWindow(VM);

                            medicalRecordWindow.ShowDialog();
                            if (medicalRecordWindow.DialogResult == true)
                            {
                                //VM.Patientmanager.UpdateMedicalRecord(VM.ClonedMedicalRecord, VM.SelectedMedicalRecord);

                                //VM.FillMedicalHistory();
                                //MedicalHistory.Items.Refresh();

                                PrintInvoice();
                            }

                        }
                        else
                        {
                            MessageBox.Show("Tisztelt betegünk az alábbi kezelést már rendezte.", "Már kifizetett kezelés", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                    return;
                }
                elem = (UIElement)VisualTreeHelper.GetParent(elem);
            }
        }
        #endregion

        #region RemoveMedicalRecord || Gombbal törlés
        // Kezelés törlése
        private void Btn_RemoveMedicalRecord_Click(object sender, RoutedEventArgs e)
        {
            VM.Patientmanager.RemoveMedicalRecord(VM.SelectedPatient, VM.SelectedMedicalRecord);
            VM.FillMedicalHistory();
            MedicalHistory.Items.Refresh();
        }
        #endregion

        #region NewProcedure || Gombbal hozzáadás


        //PROCEDURE GOMB
        private void Btn_NewProcedure_Click(object sender, RoutedEventArgs e)
        {
            if (VM.SelectedMedicalRecord.State != State.Closed) //csak akkor lehet hozzáadni, ha nem closed
            {
                ProcedureWindow procedureWindow = new ProcedureWindow(VM);
                procedureWindow.ShowDialog();
                if (procedureWindow.DialogResult == true)
                {

                    VM.Patientmanager.NewProcedure(VM.SelectedMedicalRecord, procedureWindow.Procedure);
                    VM.FillProcedures();
                    Procedures.Items.Refresh();
                }
                ////else
                ////{   //ha klónt adunk át, akkor törölhető lesz
                ////    VM.Patientmanager.RemoveProcedure(VM.SelectedMedicalRecord, procedureWindow.Procedure);
                ////}
            }
            else
            {
                MessageBox.Show("Zárt kezeléshez nem lehet eljárást rendelni!", "Node Pistikee!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        #endregion

        #region Procedure szerkesztése || Duplaklikk

        //PROCEDURE DUPLAKLIKK
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
                        if (VM.SelectedProcedure.State != State.Closed)
                        {

                            ProcedureWindow procedureWindow = new ProcedureWindow(VM.ClonedProcedure, VM);

                            procedureWindow.ShowDialog();
                            if (procedureWindow.DialogResult == true)
                            {
                                VM.Patientmanager.UpdateProcedure(VM.ClonedProcedure, VM.SelectedProcedure);
                                VM.FillProcedures();
                                Procedures.Items.Refresh();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Tisztelt betegünk az alábbi kezelést már rendezte.", "Már kifizetett kezelés", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        ////else
                        ////{   //ha klónt adunk át, akkor törölhető lesz
                        ////    VM.Patientmanager.RemoveProcedure(VM.SelectedMedicalRecord, procedureWindow.Procedure);
                        ////}

                    }

                    return;
                }
                elem = (UIElement)VisualTreeHelper.GetParent(elem);
            }
        }

        #endregion

        // Zárójelentés
        private void PrintInvoice()
        {
            if (VM.SelectedMedicalRecord != null)
            {

                List<string> bill = VM.SelectedMedicalRecord.IsClosed();

                string filename = DateTime.Now.ToString("yyyy-MM-dd-HH-mmss_") + VM.SelectedPatient.Name + ".pdf";
                string folder = @"D:\";
                VM.Patientmanager.AppManager.PrintToPDF(
                    filename,
                    folder,
                    bill);

            }

        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }

        //private void Btn_Discharge_Click(object sender, RoutedEventArgs e)
        //{

        //}

    }



}

