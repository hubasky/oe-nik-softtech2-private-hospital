using HubaskyHospitalManager.Model.ApplicationManagement;
using HubaskyHospitalManager.Model.Common;
using HubaskyHospitalManager.Model.HospitalManagement;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HubaskyHospitalManager.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }

        ApplicationManager appMgr;

        private Employee applicationUser;
        public Employee ApplicationUser 
        {
            get { return applicationUser; }
            set { applicationUser = value; OnPropertyChanged(); }
        }
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            appMgr = new ApplicationManager();
            ApplicationUser = appMgr.ApplicationUser;
            DataContext = this;

            // --- TEST CODE ---
            //appMgr.HospitalManagement = new HospitalManager(appMgr);
            //HospitalManagementWindow HospitalManagementView = new HospitalManagementWindow(appMgr.HospitalManagement);
            //HospitalManagementView.ShowDialog();

            // --- END OF TEST CODE ---
        }

        private void Grid_HospitalManagement_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            LoginWindow firstLogin = new LoginWindow(new Role[] { Role.Administrator }, appMgr);
            firstLogin.ShowDialog();
            if (firstLogin.DialogResult == true)
            {
                UpdateLoggedInUser();
                appMgr.HospitalManagement = new HospitalManager(appMgr);
                HospitalManagementWindow HospitalManagementView = new HospitalManagementWindow(appMgr.HospitalManagement);
                HospitalManagementView.ShowDialog();
                appMgr.Logout();
                UpdateLoggedInUser();
            }
        }

        private void Grid_PatientManagement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoginWindow firstLogin = new LoginWindow(appMgr);
            firstLogin.ShowDialog();
            if (firstLogin.DialogResult == true)
            {
                UpdateLoggedInUser();
                PatientManagementWindow PatientManagementView = new PatientManagementWindow();
                PatientManagementView.ShowDialog();
                appMgr.Logout();
                UpdateLoggedInUser();
            }
        }

        private void Grid_InventoryManagement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoginWindow firstLogin = new LoginWindow(new Role[] { Role.Administrator, Role.DataRecorder }, appMgr);
            firstLogin.ShowDialog();
            if (firstLogin.DialogResult == true)
            {
                UpdateLoggedInUser();
                InventoryManagementWindow InventoryManagementView = new InventoryManagementWindow();
                InventoryManagementView.ShowDialog();
                appMgr.Logout();
                UpdateLoggedInUser();
            }
        }

        private void UpdateLoggedInUser()
        {
            this.ApplicationUser = appMgr.ApplicationUser;
        }
    }
}
