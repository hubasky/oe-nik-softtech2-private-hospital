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
    public partial class NewMedicalRecordWindow : Window
    {
        public MedicalRecord MedicalRecord { get; set; }

        public NewMedicalRecordWindow()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (TxtBox_ShortDescription.Text.Equals(""))
                MessageBox.Show("Adja meg a kezelés rövid leírását!", "Hiányzó adat", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                MedicalRecord = new MedicalRecord(TxtBox_ShortDescription.Text);
                DialogResult = true;
            }
        }

    }
}
