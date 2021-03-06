﻿//merged with SB 20160430_0920
using HubaskyHospitalManager.Model.ApplicationManagement;
using HubaskyHospitalManager.Model.Common;
using HubaskyHospitalManager.Model.HospitalManagement;
using HubaskyHospitalManager.Model.InventoryManagement;
using HubaskyHospitalManager.View.HospitalManagerView;
using HubaskyHospitalManager.Model.PatientManagement;
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
using System.Threading;

namespace HubaskyHospitalManager.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Thread DbLoadThread;

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

        public Boolean? isConnected { get; set; }

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
            ////Hospitalmanager-be visz auth nélkül
            //appMgr.HospitalManagement = new HospitalManager(appMgr);
            //HospitalManagementWindow HospitalManagementView = new HospitalManagementWindow(appMgr.HospitalManagement);
            //HospitalManagementView.ShowDialog();

            //Patientmanager-be visz auth nélkül
            //appMgr.PatientManagement = new Model.PatientManagement.PatientManager(appMgr);

            //majd ezt is át kell vezetni
            //appMgr.InventoryManagement = new Model.InventoryManagement.InventoryManager(appMgr);

            //PatientManagementWindow PatientManagementView = new PatientManagementWindow(appMgr.PatientManagement);
            //PatientManagementView.ShowDialog();

            // --- END OF TEST CODE ---

            DbLoadThread = new Thread(() =>
            {
                appMgr.InitializeDataBase();
                Dispatcher.BeginInvoke((Action)(() =>
                {
                    Label_DbConnection.Content = "kapcsolódva";
                    isConnected = true;
                }));
            });

            DbLoadThread.Start(); 

        }

        private void Grid_HospitalManagement_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isConnected == true)
            {
                LoginWindow firstLogin = new LoginWindow(new Role[] { Role.Administrator, Role.DataRecorder }, appMgr);
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
            else
            {
                MessageBox.Show("Nincs kapcsolat a szerverrel.", "Hiba történt", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Grid_PatientManagement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isConnected == true)
            {
                LoginWindow firstLogin = new LoginWindow(appMgr);
                firstLogin.ShowDialog();
                if (firstLogin.DialogResult == true)
                {
                    UpdateLoggedInUser();
                    appMgr.PatientManagement = new PatientManager(appMgr);
                    appMgr.InventoryManagement = new InventoryManager(appMgr);
                    appMgr.HospitalManagement = new HospitalManager(appMgr);
                    PatientManagementWindow PatientManagementView = new PatientManagementWindow(appMgr.PatientManagement);
                    PatientManagementView.ShowDialog();
                    appMgr.Logout();
                    UpdateLoggedInUser();
                }
            }
            else
            {
                MessageBox.Show("Nincs kapcsolat a szerverrel.", "Hiba történt", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Grid_InventoryManagement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isConnected == true)
            {
                LoginWindow firstLogin = new LoginWindow(new Role[] { Role.Administrator, Role.DataRecorder }, appMgr);
                firstLogin.ShowDialog();
                if (firstLogin.DialogResult == true)
                {
                    UpdateLoggedInUser();
                    appMgr.InventoryManagement = new InventoryManager(appMgr);
                    InventoryEditorWindow InventoryEditorView = new InventoryEditorWindow(appMgr.InventoryManagement);
                    InventoryEditorView.ShowDialog();
                    appMgr.InventoryManagement = null;
                    appMgr.Logout();
                    UpdateLoggedInUser();
                }
            }
            else
            {
                MessageBox.Show("Nincs kapcsolat a szerverrel.", "Hiba történt", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateLoggedInUser()
        {
            this.ApplicationUser = appMgr.ApplicationUser;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            DbLoadThread.Interrupt();
        }
    }
}
