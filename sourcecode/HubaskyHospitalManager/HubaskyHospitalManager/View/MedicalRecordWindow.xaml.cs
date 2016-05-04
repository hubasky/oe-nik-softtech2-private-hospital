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

            VM = pmv;

            MedicalRecord = VM.ClonedMedicalRecord;

            DataContext = VM;
        }

       

        private void btnBillPay_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(checkboxBill.IsChecked.ToString());

            if (checkboxBill.IsChecked == true)
            {
                //fizetés                
                DialogResult = true;
            }
            else
            {
                //csak megtekintés
                //MedicalRecord = VM.SelectedMedicalRecord; //semmi értelme, de egy próbát megért
                DialogResult = false;
            }


        }


    }
}
