using HubaskyHospitalManager.Model.HospitalManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HubaskyHospitalManager.View.HospitalManagerView
{
    public class HospitalSelectedAddressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            UnitView unit = value as UnitView;
            Hospital hosp = null;
            if (unit != null)
                hosp = unit.Reference as Hospital;
            if (hosp != null)
                return hosp.Address;
            else
                return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
