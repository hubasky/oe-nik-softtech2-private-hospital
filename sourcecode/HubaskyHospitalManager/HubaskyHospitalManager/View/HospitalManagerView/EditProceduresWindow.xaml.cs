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

namespace HubaskyHospitalManager.View.HospitalManagerView
{
    /// <summary>
    /// Interaction logic for EditProcedures.xaml
    /// </summary>
    public partial class EditProceduresWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }

        public ApplicationManager AppMgr { get; set; }
        public Ward Ward { get; set; }

        private ObservableCollection<ProcedureType> allProcedures;
        public ObservableCollection<ProcedureType> AllProcedures
        {
            get { return allProcedures; }
            set { allProcedures = value; }
        }

        private ProcedureType allProceduresSelected;
        public ProcedureType AllProceduresSelected
        {
            get { return allProceduresSelected; }
            set { allProceduresSelected = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ProcedureType> chosenProcedures;
        public ObservableCollection<ProcedureType> ChosenProcedures
        {
            get { return chosenProcedures; }
            set { chosenProcedures = value; }
        }

        private ProcedureType chosenProceduresSelected;
        public ProcedureType ChosenProceduresSelected
        {
            get { return chosenProceduresSelected; }
            set { chosenProceduresSelected = value; OnPropertyChanged(); }
        }

        private ProcedureType editProcedure;
        public ProcedureType EditProcedure
        {
            get { return editProcedure; }
            set { editProcedure = value; OnPropertyChanged(); }
        }

        private bool isEdit = false;
        private ProcedureType sourceProcedure;

        public EditProceduresWindow()
        {
            InitializeComponent();
        }

        public EditProceduresWindow(ApplicationManager appMgr, Ward ward)
        {
            InitializeComponent();
            AppMgr = appMgr;
            Ward = ward;
            AllProcedures = new ObservableCollection<ProcedureType>();
            ChosenProcedures = new ObservableCollection<ProcedureType>();

            var procedures = AppMgr.ApplicationDb.ProcedureTypes.ToList();
            foreach (ProcedureType procedure in procedures)
                AllProcedures.Add(procedure);
            foreach (ProcedureType procedure in ward.Procedures)
            {
                ChosenProcedures.Add(procedure);
                AllProcedures.Remove(procedure);
            }

            DataContext = this;
        }

        private void Btn_AddProcedure_Click(object sender, RoutedEventArgs e)
        {
            ChosenProcedures.Add(AllProceduresSelected);
            Ward.Procedures.Add(AllProceduresSelected);
            AllProcedures.Remove(AllProceduresSelected);
            AppMgr.HospitalManagement.UpdateDatabase();
        }

        private void Btn_RemoveProcedure_Click(object sender, RoutedEventArgs e)
        {
            AllProcedures.Add(ChosenProceduresSelected);
            Ward.Procedures.Remove(ChosenProceduresSelected);
            ChosenProcedures.Remove(ChosenProceduresSelected);
            AppMgr.HospitalManagement.UpdateDatabase();
        }

        private void Btn_SaveProcedure_Click(object sender, RoutedEventArgs e)
        {
            if (isEdit)
            {
                AppMgr.HospitalManagement.UpdateProcedure(EditProcedure, sourceProcedure);
            }
            else
            {
                ProcedureType newProcedure = (ProcedureType)EditProcedure.Clone();
                AppMgr.HospitalManagement.AddNewProcedure(newProcedure);
                AllProcedures.Add(newProcedure);
            }
            EditProcedure = new ProcedureType("", "", new List<Role>());
            TxtBox_ProcId.IsEnabled = false;
            TxtBox_ProcName.IsEnabled = false;
            Chk_Admin.IsEnabled = false;
            Chk_DataRecorder.IsEnabled = false;
            Chk_Doctor.IsEnabled = false;
            Chk_Laboratorian.IsEnabled = false;
            Chk_Nurse.IsEnabled = false;
            Btn_SaveProcedure.IsEnabled = false;
            isEdit = false;
            this.InvalidateVisual();
        }

        private void Btn_NewProcedure_Click(object sender, RoutedEventArgs e)
        {
            EditProcedure = new ProcedureType("", "", new List<Role>());
            TxtBox_ProcId.IsEnabled = true;
            TxtBox_ProcName.IsEnabled = true;
            Chk_Admin.IsEnabled = true;
            Chk_DataRecorder.IsEnabled = true;
            Chk_Doctor.IsEnabled = true;
            Chk_Laboratorian.IsEnabled = true;
            Chk_Nurse.IsEnabled = true;
            Btn_SaveProcedure.IsEnabled = true;
            isEdit = false;
        }
        
        private void ListBox_AllProcedures_GotFocus(object sender, RoutedEventArgs e)
        {
            if (AllProceduresSelected != null)
            {
                EditProcedure = new ProcedureType(AllProceduresSelected.Id, AllProceduresSelected.Name, ProcedureType.GenerateRoles(AllProceduresSelected));
            }
        }

        private void ListBox_ChosenProcedures_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ChosenProceduresSelected != null)
            {
                EditProcedure = new ProcedureType(ChosenProceduresSelected.Id, ChosenProceduresSelected.Name, ProcedureType.GenerateRoles(ChosenProceduresSelected));
            }
        }

        private void Btn_DeleteProcedure_Click(object sender, RoutedEventArgs e)
        {
            if (AllProceduresSelected != null)
            {
                AppMgr.HospitalManagement.DeleteProcedure(AllProceduresSelected);
                AllProcedures.Remove(AllProceduresSelected);
                AllProceduresSelected = null;
            }
        }

        private void ListBox_AllProcedures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AllProceduresSelected != null)
            {
                EditProcedure = new ProcedureType(AllProceduresSelected.Id, AllProceduresSelected.Name, ProcedureType.GenerateRoles(AllProceduresSelected));
            }
        }

        private void ListBox_ChosenProcedures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChosenProceduresSelected != null)
            {
                EditProcedure = new ProcedureType(ChosenProceduresSelected.Id, ChosenProceduresSelected.Name, ProcedureType.GenerateRoles(ChosenProceduresSelected));
            }
        }

        private void ListBox_AllProcedures_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditSelectedProcedure(AllProceduresSelected);
        }

        private void ListBox_ChosenProcedures_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditSelectedProcedure(ChosenProceduresSelected);
        }

        private void EditSelectedProcedure(ProcedureType procedure)
        {
            if (procedure != null)
            {
                sourceProcedure = procedure;
                EditProcedure = (ProcedureType)sourceProcedure.Clone();
                TxtBox_ProcId.IsEnabled = false;
                TxtBox_ProcName.IsEnabled = true;
                Chk_Admin.IsEnabled = true;
                Chk_DataRecorder.IsEnabled = true;
                Chk_Doctor.IsEnabled = true;
                Chk_Laboratorian.IsEnabled = true;
                Chk_Nurse.IsEnabled = true;
                Btn_SaveProcedure.IsEnabled = true;
                isEdit = true;
            }
        }
    }
}
