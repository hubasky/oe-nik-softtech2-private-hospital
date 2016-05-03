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

namespace HubaskyHospitalManager.View
{
    /// <summary>
    /// Interaction logic for NewProcedureWindow.xaml
    /// </summary>
    public partial class MedicalRecordWindow : Window
    {
        public PatientManagementView VM { get; set; }
        public MedicalRecord MedicalRecord { get; set; }


        public MedicalRecordWindow(PatientManagementView pmv)
        {
            InitializeComponent();
            MedicalRecord = new MedicalRecord();

            VM = pmv;
            VM.SelectedMedicalRecord = MedicalRecord;
            DataContext = pmv;
        }

        public MedicalRecordWindow(MedicalRecord medicalRecord,PatientManagementView pmv)
        {
            InitializeComponent();

            VM = pmv;

            MedicalRecord = medicalRecord;

            DataContext = VM;            
        }

        private void Btn_ItemUsageMod_Click(object sender, RoutedEventArgs e)
        {

            InventoryManagementWindow invWindow = new InventoryManagementWindow(VM.InventoryManager);

            invWindow.ShowDialog();

            if (invWindow.DialogResult == true)
            {
                // ezt se tudom mit akar csinálni
                // VM.SelectedProcedure.UpdateInventoryUsage(VM.InventoryManager.InventoryUsage);
                //PView.ModelProcedure.UpdateInventoryUsage(VM.InventoryManager.InventoryUsage);
            }


        }

        private void Btn_NewAttachmentMod_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

    }
}
