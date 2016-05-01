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

namespace HubaskyHospitalManager.View.HospitalManagerView
{
    public class EditUnitView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }

        private Unit unit;
        public Unit Unit
        {
            get { return unit; }
            set { unit = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Employee> employees;
        public ObservableCollection<Employee> Employees
        {
            get { return employees; }
            set { employees = value; }
        }


        public EditUnitView(ApplicationManager appMgr)
        {
            unit = new Unit();
            employees = new ObservableCollection<Employee>();
            var empList = appMgr.ApplicationDb.Employees.ToList();


        }

    }
}
