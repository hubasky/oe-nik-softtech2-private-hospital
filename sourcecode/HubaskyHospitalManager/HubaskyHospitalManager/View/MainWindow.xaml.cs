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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HubaskyHospitalManager.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Grid_HospitalManagement_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            LoginWindow firstLogin = new LoginWindow();
            firstLogin.ShowDialog();
            if (firstLogin.DialogResult == true)
            {
                if (firstLogin.LoggedInUser != null)
                {
                    Status_Login.Content = "Bejelentkezve: " + firstLogin.LoggedInUser.UserName;

                    HospitalManagementWindow HospitalManagementView = new HospitalManagementWindow();
                    HospitalManagementView.ShowDialog();
                }
            }
        }

        private void Grid_PatientManagement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoginWindow firstLogin = new LoginWindow();
            firstLogin.ShowDialog();
            if (firstLogin.DialogResult == true)
            {
                if (firstLogin.LoggedInUser != null)
                {
                    Status_Login.Content = "Bejelentkezve: " + firstLogin.LoggedInUser.UserName;

                    PatientManagementWindow PatientManagementView = new PatientManagementWindow();
                    PatientManagementView.ShowDialog();
                }
            }
        }

        private void Grid_InventoryManagement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoginWindow firstLogin = new LoginWindow();
            firstLogin.ShowDialog();
            if (firstLogin.DialogResult == true)
            {
                if (firstLogin.LoggedInUser != null)
                {
                    Status_Login.Content = "Bejelentkezve: " + firstLogin.LoggedInUser.UserName;

                    InventoryManagementWindow InventoryManagementView = new InventoryManagementWindow();
                    InventoryManagementView.ShowDialog();
                }
            }
        }
    }
}
