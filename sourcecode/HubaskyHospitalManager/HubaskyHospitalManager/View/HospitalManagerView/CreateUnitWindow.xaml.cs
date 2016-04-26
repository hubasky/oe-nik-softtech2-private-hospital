using HubaskyHospitalManager.Model.ApplicationManagement;
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
    public partial class CreateUnitWindow : Window
    {
        public CreateUnitView CreatedUnitView { get; set; }
        public ApplicationManager AppMgr { get; set; }


        public CreateUnitWindow(ApplicationManager appMgr)
        {
            InitializeComponent();
            this.AppMgr = appMgr;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreatedUnitView = new CreateUnitView(AppMgr);
            DataContext = CreatedUnitView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
