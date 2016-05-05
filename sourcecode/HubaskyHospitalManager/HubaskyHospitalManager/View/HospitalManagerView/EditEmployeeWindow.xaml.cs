using HubaskyHospitalManager.Model.ApplicationManagement;
using HubaskyHospitalManager.Model.Common;
using HubaskyHospitalManager.Model.HospitalManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace HubaskyHospitalManager.View.HospitalManagerView
{
    /// <summary>
    /// Interaction logic for EditEmployeeWindow.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }

        private bool editEmployee = false;

        private Employee employee;
        public Employee Employee
        {
            get { return employee; }
            set { employee = value; OnPropertyChanged(); }
        }

        private bool isManager;
        public bool IsManager
        {
            get { return isManager; }
            set { isManager = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Unit> units;
        public ObservableCollection<Unit> Units
        {
            get { return units; }
            set { units = value; }
        }

        private Unit selectedUnit;
        public Unit SelectedUnit
        {
            get { return selectedUnit; }
            set { selectedUnit = value; OnPropertyChanged(); }
        }

        public Array RoleTypes
        {
            get { return Enum.GetNames(typeof(Role)); }
        }

        private HospitalManagementView HospView;

        public EditEmployeeWindow(HospitalManagementView view)
        {
            InitializeComponent();
            this.HospView = view;
        }

        public EditEmployeeWindow(Employee emp, HospitalManagementView view)
        {
            InitializeComponent();
            HospView = view;
            Employee = emp;
            editEmployee = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
            if (!editEmployee)
            { 
                Employee = new Employee();
            }
            else
            {
                TextBox_Username.IsEnabled = false;
                Label_Username.IsEnabled = false;
            }
            List<Unit> list = HospView.HospManager.GetUnits();
            Units = new ObservableCollection<Unit>();
            foreach (Unit unit in list)
            {
                Units.Add(unit);
            }
            if (HospView.SelectedUnit != null)
                SelectedUnit = HospView.SelectedUnit.Reference;
            if (SelectedUnit != null && SelectedUnit.Manager != null && SelectedUnit.Manager.Username.Equals(Employee.Username))
                IsManager = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool validate = true;
            string missingData = "";

            if (Employee.Name == "")
            {
                missingData += "  dolgozó neve" + Environment.NewLine;                
                validate = false;
            }
            if (Employee.Phone == "")
            {
                missingData += "  telefonszám" + Environment.NewLine;
                validate = false;
            }
            if (Employee.Address == "")
            {
                missingData += "  cím" + Environment.NewLine;
                validate = false;
            }
            if (Employee.DateOfBirth == "")
            {
                missingData += "  születési dátum" + Environment.NewLine;
                validate = false;
            }
            if (Employee.Username == "")
            {
                missingData += "  felhasználónév" + Environment.NewLine;    
                validate = false;
            }

            if (validate)
            {
                // create new user
                if (!editEmployee)
                {
                    // password is set
                    if (PswBox_Password.Password != "")
                    {
                        // password is ok
                        if (PswBox_Password.Password == PswBox_PasswordAgain.Password)
                        {
                            Employee.Password = ApplicationManager.CalculateSHA256(PswBox_Password.Password);
                            DialogResult = true;
                        }
                        // password is NOT ok
                        else
                        {
                            MessageBox.Show("A megismételt jelszó nem egyezik!", "Hibás jelszó", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    // password is NOT set
                    else
                    {
                        MessageBox.Show("Adja meg a felhasználó jelszavát!", "Hiányzó jelszó", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                // edit selected user
                else
                {
                    // password is set
                    if (PswBox_Password.Password != "")
                    {
                        // password is ok
                        if (PswBox_Password.Password == PswBox_PasswordAgain.Password)
                        {
                            Employee.Password = ApplicationManager.CalculateSHA256(PswBox_Password.Password);
                            DialogResult = true;
                        }
                        // password is NOT ok
                        else
                        {
                            MessageBox.Show("A megismételt jelszó nem egyezik!", "Hibás jelszó", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    // password is NOT set
                    else
                    {
                        DialogResult = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("Hiányzó adatok:" + Environment.NewLine + missingData, "Hiányzó adatok", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = char.IsLetter(e.Text[0]) || e.Text[0] == ' '; // e.Text[0] != ' '
        }

    }
}
