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
    public partial class ProcedureWindow : Window
    {
        public PatientManagementView VM { get; set; }
        //public ProcedureView PView { get; set; }

        public Procedure Procedure { get; set; }
        
        
        public ProcedureWindow(PatientManagementView pmv)
        {
            InitializeComponent();
            Procedure = new Procedure();

            VM = pmv;
            VM.SelectedProcedure = Procedure;
            DataContext = VM;
        }
      
        public ProcedureWindow(Procedure procedure, PatientManagementView pmv)
        {
            InitializeComponent();

            VM = pmv;
            Procedure = procedure;
            DataContext = VM;
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

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }


    }
}
