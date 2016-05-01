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
            EditUnitWindow cuw = new EditUnitWindow(AppMgr);
            cuw.ShowDialog();
            if (cuw.DialogResult == true)
            {
                Unit parent = HospView.SelectedUnit != null ? HospView.SelectedUnit.Reference : null;
                AppMgr.HospitalManagement.AddUnit(cuw.EditUnitView.Unit, parent);
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
            EditEmployeeWindow addEmp = new EditEmployeeWindow(HospView);
            addEmp.ShowDialog();
            if (addEmp.DialogResult == true)
            {
                //Unit parent = HospView.SelectedUnit != null ? HospView.SelectedUnit.Reference : null;
                Unit parent = addEmp.SelectedUnit;
                AppMgr.HospitalManagement.AddEmployee(addEmp.Employee, parent);
                if (addEmp.IsManager)
                {
                    Unit parentClone = (Unit)parent.Clone();
                    parentClone.Manager = addEmp.Employee;
                    AppMgr.HospitalManagement.UpdateUnit(parentClone, parent);
                }

                HospView.UpdateHierarchyList();
                var selection = HospView.SelectedUnit;
                HospView.SelectedUnit = null;
                HospView.SelectedUnit = selection;
            }
        }

        private void Btn_DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            AppMgr.HospitalManagement.RemoveEmployee(HospView.SelectedEmployee);
            var selection = HospView.SelectedUnit;
            HospView.SelectedUnit = null;
            HospView.SelectedUnit = selection;
            HospView.UpdateHierarchyList();
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (HospView.SelectedEmployee != null)
            {
                Employee empClone = (Employee)HospView.SelectedEmployee.Clone();
                EditEmployeeWindow editEmp = new EditEmployeeWindow(empClone, HospView);
                editEmp.ShowDialog();
                if (editEmp.DialogResult == true)
                {
                    AppMgr.HospitalManagement.UpdateEmployee(empClone, HospView.SelectedEmployee);

                    Unit origUnit = HospView.SelectedUnit.Reference;
                    Unit origUnitClone = (Unit)origUnit.Clone();
                    Unit targetUnit = editEmp.SelectedUnit;
                    Unit targetUnitClone = (Unit)targetUnit.Clone();

                    // stay
                    if (origUnit.Equals(targetUnit))
                    {
                        if (editEmp.IsManager)
                            origUnitClone.Manager = HospView.SelectedEmployee;
                        //else
                        //origUnitClone.Manager = origUnit.Manager;
                        AppMgr.HospitalManagement.UpdateUnit(origUnitClone, origUnit);
                    }
                    // move
                    else
                    {
                        if (origUnit.Manager != null)
                        {
                            if (editEmp.IsManager)
                            {
                                if (origUnit.Manager.Equals(HospView.SelectedEmployee))
                                {
                                    origUnitClone.Manager = null;
                                }
                                targetUnitClone.Manager = HospView.SelectedEmployee;
                            }
                            else
                            {
                                if (origUnit.Manager.Equals(HospView.SelectedEmployee))
                                {
                                    origUnitClone.Manager = null;
                                }
                            }
                        }
                        else
                        {
                            if (editEmp.IsManager)
                            {
                                targetUnitClone.Manager = HospView.SelectedEmployee;
                            }
                        }

                        // move employee
                        AppMgr.HospitalManagement.MoveEmployee(HospView.SelectedEmployee, HospView.SelectedUnit.Reference, targetUnit);
                        AppMgr.HospitalManagement.UpdateUnit(origUnitClone, origUnit);
                        AppMgr.HospitalManagement.UpdateUnit(targetUnitClone, targetUnit);
                    }

                    HospView.UpdateHierarchyList();
                    var selection = HospView.SelectedUnit;
                    HospView.SelectedUnit = null;
                    HospView.SelectedUnit = selection;
                }
            }
        }
        
        private void TreeView_Hierarchy_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (HospView.SelectedUnit != null)
            {
                Unit unit = HospView.SelectedUnit.Reference;
                Unit unitClone = (Unit)unit.Clone();
                EditUnitWindow editUnitWindow = new EditUnitWindow(unitClone, AppMgr);
                editUnitWindow.Title = "Szervezeti egység módosítása";
                editUnitWindow.ShowDialog();
                if (editUnitWindow.DialogResult == true)
                {
                    unitClone = editUnitWindow.EditUnitView.Unit;
                    AppMgr.HospitalManagement.UpdateUnit(unitClone, unit);
                }

                HospView.UpdateHierarchyList();
                var selection = HospView.SelectedUnit;
                HospView.SelectedUnit = null;
                HospView.SelectedUnit = selection;
            }
        }

        private void Btn_ManageProcedures_Click(object sender, RoutedEventArgs e)
        {
            if (HospView.SelectedUnit != null)
            {
                Ward ward = HospView.SelectedUnit.Reference as Ward;
                if (ward != null)
                {
                    EditProceduresWindow epw = new EditProceduresWindow(AppMgr, ward);
                    epw.ShowDialog();
                    HospView.UpdateHierarchyList();
                }
            }
        }
    }
}
