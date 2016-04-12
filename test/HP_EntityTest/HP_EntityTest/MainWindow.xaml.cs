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

namespace HP_EntityTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DataBase db = new DataBase();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_List_Click(object sender, RoutedEventArgs e)
        {
            ListEmployees();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Employee newEmp = new Employee();
            newEmp.Name = "Sanyi";
            newEmp.Phone = "+352312";
            newEmp.Role = Role.Doctor;
            // Ez a db inicializálás miatt kell
            //db.Employees.Add(newEmp);
            //db.SaveChanges();

            ListEmployees();
        }

        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWindow empWindow = new EmployeeWindow();
            empWindow.ShowDialog();
            if (empWindow.DialogResult == true)
            {
                if (empWindow.Employee != null)
                {
                    db.Employees.Add(empWindow.Employee);
                    db.SaveChanges();
                    ListEmployees();
                }
            }
        }

        private void ListEmployees()
        {
            TxtBlock_Output.Text = "";
            var emps = db.Employees.Select(m => m);
            foreach (Employee emp in emps)
            {
                TxtBlock_Output.Text += emp.Name + ", " + emp.Phone + ", " + emp.Role.ToString() + Environment.NewLine;
            }
        }
    }
}
