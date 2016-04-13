using HubaskyHospitalManager.Model.ApplicationManagement;
using HubaskyHospitalManager.Model.Common;
using HubaskyHospitalManager.Model.Exceptions;
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

namespace HubaskyHospitalManager.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        ApplicationManager appMgr;
        Role[] authRoles;
        bool isRoleAuth = false;

        public LoginWindow(ApplicationManager appMgr)
        {
            InitializeComponent();
            this.appMgr = appMgr;
        }

        public LoginWindow(Role[] roles, ApplicationManager appMgr)
        {
            InitializeComponent();
            this.appMgr = appMgr;
            this.authRoles = roles;
            this.isRoleAuth = true;
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            //appMgr.ApplicationUser = null;
            Employee user = null;
            try
            {
                if (isRoleAuth)
                    user = appMgr.Authenticate(LoginView.TxtBox_UserName.Text, LoginView.TxtBox_UserPassword.Password, authRoles);
                else
                    user = appMgr.Authenticate(LoginView.TxtBox_UserName.Text, LoginView.TxtBox_UserPassword.Password);
            }
                            
            catch (EmployeeRoleForbiddenException)
            {
                MessageBox.Show(String.Format("{0} felhasználónak nincs jogosultsága a funkció használatához!", appMgr.ApplicationUser.Username), "Figyelem!", MessageBoxButton.OK, MessageBoxImage.Warning);
                DialogResult = false;
                return;
            }
            
            if (user != null)
                LoginView.DialogResult = true;
            else
                MessageBox.Show("Hibás felhasználónév vagy jelszó", "Hibás bejelentkezés", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            LoginView.Close();
        }
    }
}
