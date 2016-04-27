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

namespace HubaskyHospitalManager.View.HospitalManagerView
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

        public ApplicationManager AppMgr { get; set; }
        HospitalManagementView HospView { get; set; }

        public HospitalManagementWindow(HospitalManager hospMgr)
        {
            InitializeComponent();
            HospView = new HospitalManagementView(hospMgr);
            AppMgr = hospMgr.AppManager;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = HospView;
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = e.NewValue as UnitView;
            if (item != null)
            {
                HospView.SelectedUnit = item;
            }
        }

        private void Btn_CreateUnit_Click(object sender, RoutedEventArgs e)
        {
            CreateUnitWindow cuw = new CreateUnitWindow(AppMgr);
            cuw.ShowDialog();
            if (cuw.DialogResult == true)
            {
                Unit parent = HospView.SelectedUnit != null ? HospView.SelectedUnit.Reference : null;
                AppMgr.HospitalManagement.AddUnit(cuw.CreatedUnitView.Unit, parent);
                HospView.UpdateHierarchyList();
            }

        }

        private void Btn_DeleteUnit_Click(object sender, RoutedEventArgs e)
        {
            if (HospView.SelectedUnit == null)
            {
                MessageBox.Show("Jelölje ki a törölni kívánt szervezeti egységet!", "Nincs kijelölve a törlésre szánt intézeti egység!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                AppMgr.HospitalManagement.RemoveUnit(HospView.SelectedUnit.Reference);
                HospView.SelectedUnit = null;
                HospView.UpdateHierarchyList();
            }            
        }

        private void Btn_AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            EditEmployeeWindow editEmp = new EditEmployeeWindow();
            editEmp.ShowDialog();
        }

        private void Btn_DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
