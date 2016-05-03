using HubaskyHospitalManager.Model.ApplicationManagement;
using HubaskyHospitalManager.Model.HospitalManagement;
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

namespace HubaskyHospitalManager.View.HospitalManagerView
{
    /// <summary>
    /// Interaction logic for CreateUnitWindow.xaml
    /// </summary>
    public partial class EditUnitWindow : Window
    {
        public EditUnitView EditUnitView { get; set; }
        public ApplicationManager AppMgr { get; set; }

        public bool IsEdit { get; set; }


        public EditUnitWindow(ApplicationManager appMgr)
        {
            InitializeComponent();
            this.AppMgr = appMgr;
            EditUnitView = new EditUnitView(AppMgr);
        }

        public EditUnitWindow(Unit unit, ApplicationManager appMgr)
        {

            InitializeComponent();
            this.AppMgr = appMgr;
            EditUnitView = new EditUnitView(AppMgr);
            if (unit != null)
                EditUnitView.Unit = unit;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = EditUnitView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Unit unit = EditUnitView.Unit;
            bool validate = true;
            string missingData = "";

            if (unit.Name == "")
            {
                missingData += "  szervezeti egység neve" + Environment.NewLine;
                validate = false;
            }

            if (unit.Email == "")
            {
                missingData += "  e-mail cím" + Environment.NewLine;
                validate = false;
            }
            if (unit.Phone == "")
            {
                missingData += "  telefonszám" + Environment.NewLine;
                validate = false;
            }
            if (unit.Web == "")
            {
                missingData += "  honalpcím" + Environment.NewLine;
                validate = false;
            }

            if (validate)
                DialogResult = true;
            else
                MessageBox.Show("Hiányzó adatok:" + Environment.NewLine + missingData, "Hiányzó adatok", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
