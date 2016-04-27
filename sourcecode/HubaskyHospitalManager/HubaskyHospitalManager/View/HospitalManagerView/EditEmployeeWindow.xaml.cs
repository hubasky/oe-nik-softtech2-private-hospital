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

        private Employee employee;
        private bool isManager;
        private ObservableCollection<Unit> units;

        public Employee Employee
        {
            get { return employee; }
            set { employee = value; OnPropertyChanged(); }
        }

        public bool IsManager
        {
            get { return isManager; }
            set { isManager = value; OnPropertyChanged(); }
        }
        
        public ObservableCollection<Unit> Units
        {
            get { return units; }
            set { units = value; }
        }

        public Array RoleTypes
        {
            get { return Enum.GetNames(typeof(Role)); }
        }

        public EditEmployeeWindow()
        {
            InitializeComponent(); 
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
