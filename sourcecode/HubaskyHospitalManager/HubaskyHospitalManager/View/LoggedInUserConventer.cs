using HubaskyHospitalManager.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HubaskyHospitalManager.View
{
    public class LoggedInUserConventer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                Employee emp = (Employee)value;
                return String.Format("Bejelentkezve: {0} ({1})", emp.Name, emp.Username);
            }
            else
                return "Nincs bejelentkezve";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
