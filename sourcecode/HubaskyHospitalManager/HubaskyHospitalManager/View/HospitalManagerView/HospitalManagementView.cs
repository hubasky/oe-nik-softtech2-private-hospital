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

namespace HubaskyHospitalManager.View.HospitalManagerView
{
    public class HospitalManagementView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }

        private HospitalManager hospManager;

        private ObservableCollection<UnitView> units;
        public ObservableCollection<UnitView> Units
        {
            get { return units; }
            set { units = value; }
        }
        

        private UnitView selectedUnit;
        public UnitView SelectedUnit
        {
            get { return selectedUnit; }
            set { selectedUnit = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Employee> employees;
        private ObservableCollection<Employee> Employees
        {
            get { return employees; }
            set { employees = value; }
        }

        private Employee selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set { selectedEmployee = value; OnPropertyChanged(); }
        }


        public HospitalManagementView(HospitalManager hospMgr)
        {
            units = new ObservableCollection<UnitView>();
            employees = new ObservableCollection<Employee>();
            hospManager = hospMgr;
            UpdateHierarchyList();
            
        }

        public void UpdateHierarchyList()
        {
            UnitView HospitalUnitView = new UnitView(hospManager.Hospital);
            var deptView = hospManager.AppManager.ApplicationDb.Departments.ToList();
            if (deptView != null)
            {
                foreach (Department dept in deptView)
                {
                    UnitView newDept = new UnitView(dept);
                    HospitalUnitView.Units.Add(newDept);
                    if (dept.Wards != null)
                    {
                        foreach (Ward ward in dept.Wards)
                        {
                            UnitView newWard = new UnitView(ward);
                            newDept.Units.Add(newWard);
                        }
                    }
                }
            }

            if (units.Count > 0)
                units.Remove(units[0]);
            units.Add(HospitalUnitView);

        }
    }
}
