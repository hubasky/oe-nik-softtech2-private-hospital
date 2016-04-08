using HubaskyHospitalManager.Model.Common;
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

        public User LoggedInUser { get; private set; }


        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            User user = new User(LoginView.TxtBox_UserName.Text, LoginView.TxtBox_UserPassword.Password);
            LoggedInUser = User.AuthenticateUser(user);
            if (LoggedInUser != null)
            {
                LoginView.DialogResult = true;
                LoginView.Close();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            LoginView.Close();
        }
    }
}
