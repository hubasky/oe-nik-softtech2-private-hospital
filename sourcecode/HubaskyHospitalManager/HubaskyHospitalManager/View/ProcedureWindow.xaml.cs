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

        private List<Ward> wardsList;
        public List<Ward> WardsList
        {
            get { return wardsList; }
            set { wardsList = value; OnPropertyChanged(); }
        }
        
        public List<Attachment> FilesToSave { get; set; }
        public List<string> LocalPathToSave { get; set; }
        
        public ProcedureWindow(PatientManagementView pmv)
        {
            InitializeComponent();

            Procedure = new Procedure();
            FilesToSave = new List<Attachment>();
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
            DataContext = this;
        }

        private void Btn_ItemUsageMod_Click(object sender, RoutedEventArgs e)
        {
            
            InventoryManagementWindow invWindow = new InventoryManagementWindow(VM.InventoryManager);

            invWindow.ShowDialog();

            if (invWindow.DialogResult == true)
            {
                foreach (InventoryItem item in VM.InventoryManager.InventoryUsage)
                {
                    ItemUsage usage = new ItemUsage(0, item.Name, item.Quantity, item.Unit);
                    Procedure.InventoryUsage.Add(usage);
                }

                ListBox_InventoryUsage.Items.Refresh();
            }


        }

        private void Btn_NewAttachmentMod_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Képek|*.jpg;*.png;*.bmp|Dokumentumok|*.docx;*.doc;*.pdf|Minden fájl|*.*";
            openDialog.Multiselect = true;
            if (openDialog.ShowDialog() == true)
            {
                FilesToSave = new List<Attachment>();
                LocalPathToSave = new List<string>();
                foreach (string file in openDialog.SafeFileNames)
                {
                    Attachment att = new Attachment(file);
                    Procedure.AddAttachment(att);
                    FilesToSave.Add(att);
                }
                foreach (string file in openDialog.FileNames)
                {
                    LocalPathToSave.Add(file);
                }

                ListBox_Attachments.Items.Refresh();
            }
        }

        // ATTACHMENT DOUBLE CLICK - save file to local
        private void ListBox_Attachments_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedAttachment != null)
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.FileName = SelectedAttachment.File;
                string[] ext = SelectedAttachment.File.Split('.');
                saveFile.Filter = "*." + ext[ext.Length - 1] + "|*." + ext[ext.Length - 1];
                saveFile.DefaultExt = ext[ext.Length - 1];
                if (saveFile.ShowDialog() == true)
                {
                    VM.Patientmanager.DownloadAttachment(SelectedAttachment, saveFile.FileName);
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }


    }
}
