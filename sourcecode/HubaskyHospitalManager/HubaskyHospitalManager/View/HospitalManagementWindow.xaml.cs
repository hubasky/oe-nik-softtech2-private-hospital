using HubaskyHospitalManager.Model.ApplicationManagement;
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

namespace HubaskyHospitalManager.View
{
    /// <summary>
    /// Interaction logic for HospitalManagementWindow.xaml
    /// </summary>
    public partial class HospitalManagementWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }

        HospitalManagementView HospView { get; set; }

        public HospitalManagementWindow(HospitalManager hospMgr)
        {
            InitializeComponent();
            HospView = new HospitalManagementView(hospMgr);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = HospView;
            // TreeView_Hierarchy.Items.Add(HospView.Units);
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = e.NewValue as UnitView;
            if (item != null)
            {
                HospView.SelectedUnit = item;
            }
        }
    }
}
