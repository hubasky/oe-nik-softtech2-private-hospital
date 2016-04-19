using HubaskyHospitalManager.Model.HospitalManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubaskyHospitalManager.View
{
    public class UnitView
    {
        public ObservableCollection<UnitView> Units { get; set; }
        public Unit Reference { get; set; }

        public UnitView(Unit unit)
        {
            Units = new ObservableCollection<UnitView>();
            Reference = unit;
        }
    }
}
