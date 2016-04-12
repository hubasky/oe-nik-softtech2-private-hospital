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

namespace HP_EntityTest
{
    /// <summary>
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public Array RoleValues
        {
            get { return Enum.GetValues(typeof(Role)); }
        }
        public Role SelectedRole { get; set; }

        public Employee Employee { get; set; }

        public EmployeeWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Employee = new Employee();
            this.Employee.Name = TxtBox_Name.Text;
            this.Employee.Phone = TxtBox_Phone.Text;
            this.Employee.Role = SelectedRole;
            this.DialogResult = true;
        }
    }
}
