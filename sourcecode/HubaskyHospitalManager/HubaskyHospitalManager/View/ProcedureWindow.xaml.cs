using System;
using System.Collections.Generic;
using System.Linq;
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
using HubaskyHospitalManager.Model.PatientManagement;
using System.IO;
using HubaskyHospitalManager.Model.Common;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HubaskyHospitalManager.Model.InventoryManagement;
using System.Collections.ObjectModel;
using HubaskyHospitalManager.Model.HospitalManagement;
using Microsoft.Win32;

namespace HubaskyHospitalManager.View
{
    /// <summary>
    /// Interaction logic for NewProcedureWindow.xaml
    /// </summary>
    public partial class ProcedureWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }

        public PatientManagementView VM { get; set; }

        private Procedure procedure;
        public Procedure Procedure
        {
            get { return procedure; }
            set { procedure = value; OnPropertyChanged(); }
        }

        public Array StateTypes
        {
            get { return Enum.GetNames(typeof(State)); }
        }

        private Attachment selectedAttachment;
        public Attachment SelectedAttachment
        {
            get { return selectedAttachment; }
            set { selectedAttachment = value; OnPropertyChanged(); }
        }

        private InventoryItem selectedItem;
        public InventoryItem SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; OnPropertyChanged(); }
        }

        private List<Ward> wardsList;
        public List<Ward> WardsList
        {
            get { return wardsList; }
            set { wardsList = value; OnPropertyChanged(); }
        }

        public List<Attachment> FilesToSave { get; set; }
        public List<Attachment> FilesToDelete { get; set; }
        
        public ProcedureWindow(PatientManagementView pmv)
        {
            InitializeComponent();

            Procedure = new Procedure();
            FilesToSave = new List<Attachment>();
            FilesToDelete = new List<Attachment>();
            WardsList = pmv.Patientmanager.AppManager.ApplicationDb.Wards.ToList();
            VM = pmv;
            //VM.SelectedProcedure = Procedure;
            DataContext = this;
        }
      
        public ProcedureWindow(Procedure procedure, PatientManagementView pmv)
        {
            InitializeComponent();

            VM = pmv;
            Procedure = (Procedure)procedure.Clone();
            FilesToSave = new List<Attachment>();
            FilesToDelete = new List<Attachment>();
            DataContext = this;
        }

        private void Btn_ItemUsageMod_Click(object sender, RoutedEventArgs e)
        {
            
            InventoryManagementWindow invWindow = new InventoryManagementWindow(VM.InventoryManager);

            invWindow.ShowDialog();

            if (invWindow.DialogResult == true)
            {
                //PView.ModelProcedure.UpdateInventoryUsage(VM.InventoryManager.InventoryUsage);
                // ez itt nem tudom mit akar csinálni...
                //VM.SelectedProcedure.UpdateInventoryUsage(VM.InventoryManager.InventoryUsage);
            }


        }

        private void Btn_NewAttachmentMod_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Képek|*.jpg;*.png;*.bmp|Dokumentumok|*.docx;*.doc;*.pdf|Minden fájl|*.*";
            openDialog.Multiselect = true;
            if (openDialog.ShowDialog() == true)
            {
                foreach (string file in openDialog.SafeFileNames)
                {
                    Attachment att = new Attachment(file);
                    Procedure.AddAttachment(att);
                    FilesToSave.Add(att);
                }
                ListBox_Attachments.Items.Refresh();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
